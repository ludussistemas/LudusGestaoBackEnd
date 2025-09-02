using System.Threading.Tasks;

namespace LudusGestao.Domain.Interfaces.Services.infra
{
    public interface ITenantCreationService
    {
        Task<int> GenerateNewTenantIdAsync();
    }
}
