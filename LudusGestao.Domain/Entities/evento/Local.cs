using LudusGestao.Core.Entities.Base;
using LudusGestao.Domain.Entities.geral;
using LudusGestao.Domain.Enums.eventos;

namespace LudusGestao.Domain.Entities.eventos
{
    public class Local : BaseEntity, ITenantEntity, IFilialEntity
    {
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public int Intervalo { get; set; }
        public decimal ValorHora { get; set; }
        public int? Capacidade { get; set; }
        public string Descricao { get; set; }
        public List<string> Comodidades { get; set; } = new List<string>();
        public SituacaoLocal Situacao { get; set; }
        public string Cor { get; set; }
        public string HoraAbertura { get; set; }
        public string HoraFechamento { get; set; }
        public Guid FilialId { get; set; }
        public int TenantId { get; set; }
    }
}
