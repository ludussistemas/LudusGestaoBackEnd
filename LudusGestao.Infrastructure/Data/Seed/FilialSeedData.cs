using LudusGestao.Domain.Entities.geral;
using LudusGestao.Domain.Enums.geral;

namespace LudusGestao.Infrastructure.Data.Seed
{
    public static class FilialSeedData
    {
        public static Filial GetFilial(Guid empresaId, int tenantId)
        {
            return new Filial
            {
                Id = Guid.NewGuid(),
                Nome = "Filial Central",
                Codigo = "F001",
                Endereco = "Rua Central, 200",
                Cidade = "CidadeX",
                Estado = "SP",
                Cep = "00000-001",
                Telefone = "(11) 88888-8888",
                Email = "filial@ludus.com.br",
                Cnpj = "12345678000199",
                Responsavel = "João Gerente",
                DataAbertura = DateTime.UtcNow,
                Situacao = SituacaoBase.Ativo,
                EmpresaId = empresaId,
                TenantId = tenantId,
                DataCriacao = DateTime.UtcNow
            };
        }
    }
}
