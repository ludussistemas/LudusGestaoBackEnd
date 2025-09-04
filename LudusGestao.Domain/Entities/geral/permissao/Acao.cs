using LudusGestao.Core.Entities.Base;

namespace LudusGestao.Domain.Entities.geral.permissao
{
    public class Acao : BaseEntity
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}


