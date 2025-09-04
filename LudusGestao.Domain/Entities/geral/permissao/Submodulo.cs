using LudusGestao.Core.Entities.Base;

namespace LudusGestao.Domain.Entities.geral.permissao
{
    public class Submodulo : BaseEntity
    {
        public Guid ModuloId { get; set; }
        public Modulo Modulo { get; set; }
        public string Nome { get; set; }
    }
}


