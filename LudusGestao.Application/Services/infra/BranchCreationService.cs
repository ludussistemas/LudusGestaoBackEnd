using LudusGestao.Core.Constants;
using LudusGestao.Domain.Entities.geral;
using LudusGestao.Domain.Enums.geral;
using LudusGestao.Domain.Interfaces.Repositories.geral;
using LudusGestao.Domain.Interfaces.Services;

namespace LudusGestao.Application.Services
{
    public class BranchCreationService : IBranchCreationService
    {
        private readonly IFilialRepository _filialRepository;

        public BranchCreationService(IFilialRepository filialRepository)
        {
            _filialRepository = filialRepository;
        }

        public async Task<Filial> CreateMainBranchAsync(string nome, string codigo, Guid companyId, int tenantId)
        {
            var filial = new Filial
            {
                Id = Guid.NewGuid(),
                Nome = $"Matriz {nome}",
                Codigo = codigo,
                Endereco = "",
                Cidade = "",
                Estado = "",
                Cep = "",
                Telefone = "",
                Email = $"matriz@{nome.Replace(" ", "").ToLower()}.com.br",
                Cnpj = "",
                Responsavel = UserConstants.DefaultBranchResponsible,
                Situacao = SituacaoBase.Ativo,
                DataAbertura = DateTime.UtcNow,
                DataCriacao = DateTime.UtcNow,
                DataAtualizacao = DateTime.UtcNow,
                TenantId = tenantId,
                EmpresaId = companyId
            };

            await _filialRepository.Criar(filial);
            return filial;
        }
    }
}
