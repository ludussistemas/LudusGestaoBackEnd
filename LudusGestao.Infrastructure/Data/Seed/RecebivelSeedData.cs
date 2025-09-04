using LudusGestao.Domain.Entities.eventos;
using LudusGestao.Domain.Enums.eventos;

namespace LudusGestao.Infrastructure.Data.Seed
{
    public static class RecebivelSeedData
    {
        public static Recebivel GetRecebivelExemplo(Guid clienteId, Guid? reservaId, Guid filialId, int tenantId)
        {
            return new Recebivel
            {
                Id = Guid.NewGuid(),
                ClienteId = clienteId,
                Descricao = "Recebimento teste",
                Valor = 100.00m,
                DataVencimento = DateTime.UtcNow.Date.AddDays(30),
                Situacao = SituacaoRecebivel.Aberto,
                ReservaId = reservaId,
                FilialId = filialId,
                TenantId = tenantId,
                DataCriacao = DateTime.UtcNow
            };
        }
    }
}
