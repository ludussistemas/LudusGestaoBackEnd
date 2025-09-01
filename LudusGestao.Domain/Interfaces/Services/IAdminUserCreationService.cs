using System;
using System.Threading.Tasks;
using LudusGestao.Domain.Entities.geral;
using LudusGestao.Domain.Entities;

namespace LudusGestao.Domain.Interfaces.Services
{
    public interface IAdminUserCreationService
    {
        Task<Usuario> CreateAdminUserAsync(string nome, string email, string senha, Guid companyId, int tenantId);
    }
}
