using LudusGestao.Core.Interfaces.Repositories.Base;
using LudusGestao.Domain.Entities.geral;

namespace LudusGestao.Domain.Interfaces.Repositories.geral
{
    public interface IUsuarioPermissaoFilialRepository : IBaseRepository<UsuarioPermissaoFilial>
    {
        Task<UsuarioPermissaoFilial?> ObterPorUsuarioEFilialAsync(Guid usuarioId, Guid filialId);
        Task<IEnumerable<UsuarioPermissaoFilial>> ObterPorUsuarioAsync(Guid usuarioId);
        Task<IEnumerable<Guid>> ObterFiliaisPorUsuarioAsync(Guid usuarioId);
        Task<IEnumerable<UsuarioPermissaoFilial>> ObterPorFilialAsync(Guid filialId);
        Task<IEnumerable<UsuarioPermissaoFilial>> ObterPermissoesPorUsuarioAsync(Guid usuarioId);
    }
}
