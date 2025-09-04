using LudusGestao.Core.Entities.Base;
using LudusGestao.Domain.Enums.eventos;

namespace LudusGestao.Domain.Entities.eventos
{
    public class Cliente : BaseEntity, ITenantEntity, IFilialEntity
    {
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string Observacoes { get; set; }
        public SituacaoCliente Situacao { get; set; }
        public Guid FilialId { get; set; }
        public int TenantId { get; set; }
    }
}
