using LudusGestao.Domain.Entities.eventos;
using LudusGestao.Domain.Entities.geral;
using LudusGestao.Domain.Enums.eventos;

namespace LudusGestao.Infrastructure.Data.Seed
{
    public static class ClienteSeedData
    {
        public static Cliente GetClienteExemplo(Guid filialId, int tenantId)
        {
            return new Cliente
            {
                Id = Guid.NewGuid(),
                Nome = "Cliente Exemplo",
                Documento = "12345678901",
                Email = "cliente@exemplo.com",
                Telefone = "(11) 66666-6666",
                Endereco = "Rua Cliente, 300",
                Observacoes = "Cliente VIP",
                Situacao = SituacaoCliente.Ativo,
                DataCriacao = DateTime.UtcNow,
                FilialId = filialId,
                TenantId = tenantId,
            };
        }
    }
}
