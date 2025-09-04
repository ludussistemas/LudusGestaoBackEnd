using LudusGestao.Domain.Entities.eventos;
using LudusGestao.Domain.Enums.eventos;

namespace LudusGestao.Infrastructure.Data.Seed
{
    public static class ReservaSeedData
    {
        public static Reserva GetReservaExemplo(Guid clienteId, Guid localId, Guid filialId, int tenantId)
        {
            return new Reserva
            {
                Id = Guid.NewGuid(),
                ClienteId = clienteId,
                LocalId = localId,
                DataInicio = DateTime.UtcNow.Date.AddDays(1).AddHours(10),
                DataFim = DateTime.UtcNow.Date.AddDays(1).AddHours(11),
                Situacao = SituacaoReserva.Confirmada,
                Valor = 100.00m,
                Observacoes = "Reserva teste",
                FilialId = filialId,
                TenantId = tenantId,
                DataCriacao = DateTime.UtcNow
            };
        }
    }
}
