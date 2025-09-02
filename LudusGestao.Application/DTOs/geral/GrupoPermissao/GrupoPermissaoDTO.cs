using LudusGestao.Domain.Enums.geral;

namespace LudusGestao.Application.DTOs.geral.GrupoPermissao
{
    public class GrupoPermissaoDTO
    {
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public SituacaoBase Situacao { get; set; }
        public int TenantId { get; set; }
    }
}