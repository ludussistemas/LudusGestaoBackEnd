using LudusGestao.Domain.Entities.geral;
using LudusGestao.Domain.Interfaces.Repositories.Base;
using System.Threading.Tasks;

namespace LudusGestao.Domain.Interfaces.Repositories.geral
{
    public interface IEmpresaRepository : IBaseRepository<Empresa>
    {
        Task<int> GetMaxTenantIdAsync();
    }
} 