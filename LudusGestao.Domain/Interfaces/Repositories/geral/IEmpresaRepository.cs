using LudusGestao.Core.Interfaces.Repositories.Base;
using LudusGestao.Domain.Entities.geral;

namespace LudusGestao.Domain.Interfaces.Repositories.geral
{
    public interface IEmpresaRepository : IBaseRepository<Empresa>
    {
        Task<int> GetMaxTenantIdAsync();
    }
}
