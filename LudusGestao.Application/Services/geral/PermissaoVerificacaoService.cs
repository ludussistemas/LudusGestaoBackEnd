using LudusGestao.Domain.Interfaces.Repositories.geral;
using LudusGestao.Domain.Entities.geral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<bool> UsuarioTemPermissaoAsync(Guid usuarioId, string permissao, Guid? filialId = null)
        {
            // 1. Verificar se usuário existe
            var usuario = await _usuarioRepository.ObterPorId(usuarioId);
            if (usuario == null) return false;

            // 2. Obter permissões do usuário
            var permissoesUsuario = await ObterPermissoesUsuarioAsync(usuarioId, filialId);
            
            // 3. Verificar se tem a permissão específica
            return permissoesUsuario.Contains(permissao);
        }

        public async Task<bool> UsuarioTemAcessoModuloAsync(Guid usuarioId, string moduloPai)
        {
            // Verificar se usuário tem acesso ao módulo pai
            var permissaoModulo = $"{moduloPai}.acesso";
            return await UsuarioTemPermissaoAsync(usuarioId, permissaoModulo);
        }

        public async Task<bool> UsuarioTemAcessoFilialAsync(Guid usuarioId, Guid filialId)
        {
            // Verificar se usuário tem qualquer permissão para a filial
            var filiaisUsuario = await ObterFiliaisUsuarioAsync(usuarioId);
            return filiaisUsuario.Contains(filialId);
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

        public async Task<IEnumerable<Guid>> ObterFiliaisUsuarioAsync(Guid usuarioId)
        {
            var filiais = new HashSet<Guid>();

            // 1. Obter usuário
            var usuario = await _usuarioRepository.ObterPorId(usuarioId);
            if (usuario == null) return filiais;

            // 2. Se tem grupo, obter filiais do grupo
            if (usuario.GrupoPermissaoId.HasValue)
            {
                var filiaisGrupo = await _grupoFilialRepository.ObterFiliaisPorGrupoAsync(usuario.GrupoPermissaoId.Value);
                foreach (var filial in filiaisGrupo)
                {
                    filiais.Add(filial);
                }
            }

            // 3. Obter filiais individuais do usuário
            var filiaisIndividuais = await _usuarioFilialRepository.ObterFiliaisPorUsuarioAsync(usuarioId);
            foreach (var filial in filiaisIndividuais)
            {
                filiais.Add(filial);
            }

            return filiais;
        }

        public async Task<IEnumerable<string>> ObterModulosUsuarioAsync(Guid usuarioId)
        {
            var permissoes = await ObterPermissoesUsuarioAsync(usuarioId);
            var modulos = new HashSet<string>();

            foreach (var permissao in permissoes)
            {
                if (permissao.EndsWith(".acesso"))
                {
                    var modulo = permissao.Replace(".acesso", "");
                    modulos.Add(modulo);
                }
            }

            return modulos;
        }

        private async Task<IEnumerable<string>> ObterPermissoesGrupoAsync(Guid grupoId, Guid? filialId = null)
        {
            var permissoes = new List<string>();

            if (filialId.HasValue)
            {
                // Permissões específicas da filial
                var grupoFilial = await _grupoFilialRepository.ObterPorGrupoEFilialAsync(grupoId, filialId.Value);
                if (grupoFilial != null)
                {
                    var permissoesEntidades = await _permissaoRepository.ObterPorIdsAsync(grupoFilial.Permissoes);
                    permissoes.AddRange(permissoesEntidades.Select(p => p.Nome));
                }
            }
            else
            {
                // Todas as permissões do grupo (todas as filiais)
                var gruposFiliais = await _grupoFilialRepository.ObterPorGrupoAsync(grupoId);
                foreach (var grupoFilial in gruposFiliais)
                {
                    var permissoesEntidades = await _permissaoRepository.ObterPorIdsAsync(grupoFilial.Permissoes);
                    permissoes.AddRange(permissoesEntidades.Select(p => p.Nome));
                }
            }

            return permissoes.Distinct();
        }

        private async Task<IEnumerable<string>> ObterPermissoesIndividuaisAsync(Guid usuarioId, Guid? filialId = null)
        {
            var permissoes = new List<string>();

            if (filialId.HasValue)
            {
                // Permissões específicas da filial
                var usuarioFilial = await _usuarioFilialRepository.ObterPorUsuarioEFilialAsync(usuarioId, filialId.Value);
                if (usuarioFilial != null)
                {
                    var permissoesEntidades = await _permissaoRepository.ObterPorIdsAsync(usuarioFilial.Permissoes);
                    permissoes.AddRange(permissoesEntidades.Select(p => p.Nome));
                }
            }
            else
            {
                // Todas as permissões individuais (todas as filiais)
                var usuariosFiliais = await _usuarioFilialRepository.ObterPorUsuarioAsync(usuarioId);
                foreach (var usuarioFilial in usuariosFiliais)
                {
                    var permissoesEntidades = await _permissaoRepository.ObterPorIdsAsync(usuarioFilial.Permissoes);
                    permissoes.AddRange(permissoesEntidades.Select(p => p.Nome));
                }
            }

            return permissoes.Distinct();
        }
    }
}
