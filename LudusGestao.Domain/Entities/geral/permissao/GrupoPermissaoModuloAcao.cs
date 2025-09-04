using LudusGestao.Core.Entities.Base;

namespace LudusGestao.Domain.Entities.geral.permissao
{
    public class GrupoPermissaoModuloAcao : BaseEntity
    {
        public Guid GrupoId { get; set; }
        public GrupoPermissao Grupo { get; set; }
        public Guid ModuloId { get; set; }
        public Modulo Modulo { get; set; }
        public Guid AcaoId { get; set; }
        public Acao Acao { get; set; }
        public bool Permitido { get; set; }
    }
}


