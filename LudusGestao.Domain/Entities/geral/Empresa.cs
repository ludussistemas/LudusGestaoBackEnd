using LudusGestao.Core.Entities.Base;
using LudusGestao.Domain.Enums.geral;

namespace LudusGestao.Domain.Entities.geral
{
    public class Empresa : BaseEntity, ITenantEntity
    {
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }
        public string Telefone { get; set; }
        public ICollection<Filial> Filiais { get; set; }
        public int TenantId { get; set; }
        public SituacaoBase Situacao { get; set; }
    }
}
