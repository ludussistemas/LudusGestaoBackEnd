using LudusGestao.Domain.Entities.geral;

namespace LudusGestao.Domain.Interfaces.Services
{
    public interface ICompanyCreationService
    {
        Task<Empresa> CreateCompanyAsync(string nome, string cnpj, int tenantId);
    }
}
