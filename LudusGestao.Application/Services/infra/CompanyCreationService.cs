using System;
using System.Threading.Tasks;
using LudusGestao.Domain.Interfaces.Services;
using LudusGestao.Domain.Interfaces.Repositories.geral;
using LudusGestao.Application.DTOs.Gerencialmento;
using LudusGestao.Domain.Entities.geral;
using LudusGestao.Domain.Enums.geral;

namespace LudusGestao.Application.Services
{
    public class CompanyCreationService : ICompanyCreationService
    {
        private readonly IEmpresaRepository _empresaRepository;

        public CompanyCreationService(IEmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }

        public async Task<Empresa> CreateCompanyAsync(string nome, string cnpj, int tenantId)
        {
            var empresa = new Empresa
            {
                Id = Guid.NewGuid(),
                Nome = nome,
                Cnpj = cnpj,
                Email = "",
                Endereco = "",
                Cidade = "",
                Estado = "",
                Cep = "",
                Telefone = "",
                Situacao = SituacaoBase.Ativo,
                TenantId = tenantId
            };

            await _empresaRepository.Criar(empresa);
            return empresa;
        }
    }
}
