using System;
using System.Threading.Tasks;
using LudusGestao.Domain.Entities.geral;
using LudusGestao.Domain.Entities;

namespace LudusGestao.Domain.Interfaces.Services
{
    public interface ICompanyCreationService
    {
        Task<Empresa> CreateCompanyAsync(string nome, string cnpj, int tenantId);
    }
}
