using LudusGestao.Domain.Enums;
using LudusGestao.Domain.Enums.geral;

namespace LudusGestao.Application.DTOs.geral.GrupoPermissao
{
    public class CreateGrupoPermissaoDTO
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public SituacaoBase Situacao { get; set; } = SituacaoBase.Ativo;
    }
} 