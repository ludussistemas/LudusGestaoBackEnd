using System;
using System.Threading.Tasks;
using LudusGestao.Domain.Entities.geral;
using LudusGestao.Domain.Entities;

namespace LudusGestao.Domain.Interfaces.Services
{
    public interface IBranchCreationService
    {
        Task<Filial> CreateMainBranchAsync(string nome, string codigo, Guid companyId, int tenantId);
    }
}
