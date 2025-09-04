using LudusGestao.Domain.Enums.geral;

namespace LudusGestao.Domain.DTOs.geral.GrupoPermissao
{
    public class UpdateGrupoPermissaoDTO
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public SituacaoBase Situacao { get; set; }
    }
}
