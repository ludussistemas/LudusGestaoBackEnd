using LudusGestao.Domain.Entities.geral;

namespace LudusGestao.Domain.Interfaces.Services.infra
{
    public interface IAdminUserCreationService
    {
        Task<Usuario> CreateAdminUserAsync(string nome, string email, string senha, Guid companyId, int tenantId);
    }
}
