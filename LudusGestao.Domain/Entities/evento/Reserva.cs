using LudusGestao.Core.Entities.Base;
using LudusGestao.Domain.Enums.eventos;

namespace LudusGestao.Domain.Entities.eventos
{
    public class Reserva : BaseEntity, ITenantEntity, IFilialEntity
    {
        public Guid ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;
        public Guid LocalId { get; set; }
        public Local Local { get; set; } = null!;
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public SituacaoReserva Situacao { get; set; }
        public decimal Valor { get; set; }
        public string? Observacoes { get; set; }
        public Guid FilialId { get; set; }
        public int TenantId { get; set; }
    }
}
