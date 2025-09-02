using LudusGestao.Domain.Entities.geral;

namespace LudusGestao.Domain.Interfaces.Services
{
    public interface IBranchCreationService
    {
        Task<Filial> CreateMainBranchAsync(string nome, string codigo, Guid companyId, int tenantId);
    }
}
