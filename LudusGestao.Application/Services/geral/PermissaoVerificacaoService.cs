using LudusGestao.Domain.Interfaces.Repositories.geral;
using LudusGestao.Domain.Interfaces.Services.geral;

namespace LudusGestao.Application.Services
{
    public class PermissaoVerificacaoService : IPermissaoVerificacaoService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IGrupoPermissaoRepository _grupoRepository;
        private readonly IPermissaoRepository _permissaoRepository;
        private readonly IGrupoPermissaoFilialRepository _grupoFilialRepository;
        private readonly IUsuarioPermissaoFilialRepository _usuarioFilialRepository;

        public PermissaoVerificacaoService(
            IUsuarioRepository usuarioRepository,
            IGrupoPermissaoRepository grupoRepository,
            IPermissaoRepository permissaoRepository,
            IGrupoPermissaoFilialRepository grupoFilialRepository,
            IUsuarioPermissaoFilialRepository usuarioFilialRepository)
        {
            _usuarioRepository = usuarioRepository;
            _grupoRepository = grupoRepository;
            _permissaoRepository = permissaoRepository;
            _grupoFilialRepository = grupoFilialRepository;
            _usuarioFilialRepository = usuarioFilialRepository;
        }

        public async Task<bool> VerificarPermissaoUsuarioAsync(Guid usuarioId, string permissao, Guid? filialId = null)
        {
            // 1. Verificar se usuário existe
            var usuario = await _usuarioRepository.ObterPorId(usuarioId);
            if (usuario == null) return false;

            // 2. Obter permissões do usuário
            var permissoesUsuario = await ObterPermissoesUsuarioAsync(usuarioId, filialId);

            // 3. Verificar se tem a permissão específica
            return permissoesUsuario.Contains(permissao);
        }

        public async Task<IEnumerable<string>> ObterPermissoesUsuarioAsync(Guid usuarioId, Guid? filialId = null)
        {
            var permissoes = new HashSet<string>();

            // 1. Obter usuário
            var usuario = await _usuarioRepository.ObterPorId(usuarioId);
            if (usuario == null) return permissoes;

            // 2. Se tem grupo de permissões, obter permissões do grupo
            if (usuario.GrupoPermissaoId.HasValue)
            {
                var permissoesGrupo = await ObterPermissoesGrupoAsync(usuario.GrupoPermissaoId.Value, filialId);
                foreach (var permissao in permissoesGrupo)
                {
                    permissoes.Add(permissao);
                }
            }

            // 3. Obter permissões individuais do usuário
            var permissoesIndividuais = await ObterPermissoesIndividuaisAsync(usuarioId, filialId);
            foreach (var permissao in permissoesIndividuais)
            {
                permissoes.Add(permissao);
            }

            return permissoes;
        }

        private async Task<IEnumerable<string>> ObterPermissoesGrupoAsync(Guid grupoId, Guid? filialId)
        {
            var permissoes = new List<string>();

            // Obter grupo
            var grupo = await _grupoRepository.ObterPorId(grupoId);
            if (grupo == null) return permissoes;

            // Obter permissões do grupo
            var permissoesGrupo = await _permissaoRepository.ObterPermissoesPorGrupoAsync(grupoId);
            foreach (var permissao in permissoesGrupo)
            {
                permissoes.Add(permissao.Nome);
            }

            // Se especificou filial, filtrar permissões da filial
            if (filialId.HasValue)
            {
                var permissoesFilial = await _grupoFilialRepository.ObterPermissoesPorGrupoEFilialAsync(grupoId, filialId.Value);
                var permissoesFiltradas = permissoes.Where(p => permissoesFilial.Any(pf => pf.Nome == p));
                return permissoesFiltradas;
            }

            return permissoes;
        }

        private async Task<IEnumerable<string>> ObterPermissoesIndividuaisAsync(Guid usuarioId, Guid? filialId)
        {
            var permissoes = new List<string>();

            // Obter permissões individuais do usuário
            var permissoesUsuario = await _usuarioFilialRepository.ObterPermissoesPorUsuarioAsync(usuarioId);

            foreach (var permissaoUsuario in permissoesUsuario)
            {
                // Se especificou filial, verificar se a permissão é para essa filial
                if (filialId.HasValue)
                {
                    if (permissaoUsuario.FilialId == filialId.Value)
                    {
                        // Obter as permissões pelos IDs
                        var permissoesIds = permissaoUsuario.Permissoes;
                        var permissoesEntidades = await _permissaoRepository.ObterPorIdsAsync(permissoesIds);
                        foreach (var permissao in permissoesEntidades)
                        {
                            permissoes.Add(permissao.Nome);
                        }
                    }
                }
                else
                {
                    // Obter as permissões pelos IDs
                    var permissoesIds = permissaoUsuario.Permissoes;
                    var permissoesEntidades = await _permissaoRepository.ObterPorIdsAsync(permissoesIds);
                    foreach (var permissao in permissoesEntidades)
                    {
                        permissoes.Add(permissao.Nome);
                    }
                }
            }

            return permissoes;
        }
    }
}
