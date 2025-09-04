using LudusGestao.Domain.Entities.geral;
using LudusGestao.Domain.Enums.geral;

namespace LudusGestao.Infrastructure.Data.Seed
{
    public static class EmpresaSeedData
    {
        public static Empresa GetEmpresa(int tenantId)
        {
            return new Empresa
            {
                Id = Guid.NewGuid(),
                Nome = "Ludus Sistemas",
                Cnpj = "12345678000199",
                Email = "contato@ludus.com.br",
                Endereco = "Rua Exemplo, 100",
                Cidade = "CidadeX",
                Estado = "SP",
                Cep = "00000-000",
                Telefone = "(11) 99999-9999",
                Situacao = SituacaoBase.Ativo,
                TenantId = tenantId,
                DataCriacao = DateTime.UtcNow
            };
        }
    }
}
