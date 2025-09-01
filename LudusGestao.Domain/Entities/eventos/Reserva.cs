using LudusGestao.Domain.Entities.Base;
using LudusGestao.Domain.Entities.geral;
using LudusGestao.Domain.Entities.eventos;
using LudusGestao.Domain.Enums.eventos;
using System;

namespace LudusGestao.Domain.Entities.eventos
{
    public class Reserva : BaseEntity, ITenantEntity
    {
        public Guid ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;
        public Guid LocalId { get; set; }
        public Local Local { get; set; } = null!;
        public Guid? UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public DateTime Data { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFim { get; set; }
        public SituacaoReserva Situacao { get; set; }
        public string Cor { get; set; }
        public string Esporte { get; set; }
        public string Observacoes { get; set; }
        public decimal Valor { get; set; }
        public int TenantId { get; set; }
    }
} 