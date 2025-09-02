using LudusGestao.Domain.Entities.Base;
using LudusGestao.Domain.Enums.eventos;
using System;

namespace LudusGestao.Domain.Entities.eventos
{
    public class Recebivel : BaseEntity, ITenantEntity
    {
        public Guid ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public SituacaoRecebivel Situacao { get; set; }
        public Guid? ReservaId { get; set; }
        public Reserva? Reserva { get; set; }
        public int TenantId { get; set; }
    }
} 