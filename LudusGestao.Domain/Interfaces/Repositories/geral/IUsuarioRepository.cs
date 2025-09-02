using LudusGestao.Core.Interfaces.Repositories.Base;
using LudusGestao.Domain.Entities.geral;

namespace LudusGestao.Domain.Interfaces.Repositories.geral
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Task<Usuario?> ObterPorEmail(string email);
        Task<Usuario?> ObterPorEmailGlobal(string email);
        Task<IEnumerable<Usuario>> ObterPorEmpresa(Guid empresaId);
        Task<IEnumerable<Usuario>> ObterPorGrupo(Guid grupoId);
        Task<Usuario?> GetByEmailAsync(string email);
    }
}
