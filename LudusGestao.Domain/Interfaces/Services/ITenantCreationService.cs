using System.Threading.Tasks;

namespace LudusGestao.Domain.Interfaces.Services
{
    public interface ITenantCreationService
    {
        Task<int> GenerateNewTenantIdAsync();
    }
}
