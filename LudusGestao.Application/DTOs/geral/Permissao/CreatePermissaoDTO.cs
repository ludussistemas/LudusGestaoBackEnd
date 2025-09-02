using LudusGestao.Domain.Enums.geral;

namespace LudusGestao.Application.DTOs.geral.Permissao
{
    public class CreatePermissaoDTO
    {
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string ModuloPai { get; set; } = string.Empty;
        public string Submodulo { get; set; } = string.Empty;
        public string Acao { get; set; } = string.Empty;
        public SituacaoBase Situacao { get; set; } = SituacaoBase.Ativo;
    }
}
