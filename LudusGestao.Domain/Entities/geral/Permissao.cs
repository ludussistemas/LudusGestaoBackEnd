using LudusGestao.Core.Entities.Base;
using LudusGestao.Domain.Enums.geral;

namespace LudusGestao.Domain.Entities.geral
{
    public class Permissao : BaseEntity, ITenantEntity
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string ModuloPai { get; set; }
        public string Submodulo { get; set; }
        public string Acao { get; set; }
        public SituacaoBase Situacao { get; set; }
        public int TenantId { get; set; }
    }
}
