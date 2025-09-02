using LudusGestao.Core.Interfaces.Repositories.Base;
using LudusGestao.Domain.Entities.geral;

namespace LudusGestao.Domain.Interfaces.Repositories.geral
{
    public interface IGrupoPermissaoRepository : IBaseRepository<GrupoPermissao>
    {
        Task<IEnumerable<GrupoPermissao>> ObterComUsuarios();
        Task<GrupoPermissao?> ObterComUsuariosPorId(Guid id);
        Task<IEnumerable<Usuario>> ObterUsuariosDoGrupoAsync(Guid grupoId);
        Task AdicionarUsuarioAoGrupoAsync(Guid grupoId, Guid usuarioId);
        Task RemoverUsuarioDoGrupoAsync(Guid grupoId, Guid usuarioId);
    }
}
