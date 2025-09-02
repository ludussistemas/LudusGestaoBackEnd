using LudusGestao.Domain.Interfaces.Repositories.geral;
using LudusGestao.Domain.Interfaces.Services.geral;

namespace LudusGestao.Application.Services
{
    public class FilialAcessoService : IFilialAcessoService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IGrupoPermissaoFilialRepository _grupoFilialRepository;
        private readonly IUsuarioPermissaoFilialRepository _usuarioFilialRepository;

        public FilialAcessoService(
            IUsuarioRepository usuarioRepository,
            IGrupoPermissaoFilialRepository grupoFilialRepository,
            IUsuarioPermissaoFilialRepository usuarioFilialRepository)
        {
            _usuarioRepository = usuarioRepository;
            _grupoFilialRepository = grupoFilialRepository;
            _usuarioFilialRepository = usuarioFilialRepository;
        }

        public async Task<bool> VerificarAcessoFilialAsync(Guid usuarioId, Guid filialId)
        {
            // Verificar se usuário tem qualquer permissão para a filial
            var filiaisUsuario = await ObterFiliaisUsuarioAsync(usuarioId);
            return filiaisUsuario.Contains(filialId);
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
    }
}
