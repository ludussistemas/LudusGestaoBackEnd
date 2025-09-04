using LudusGestao.Core.Entities.Base;

namespace LudusGestao.Domain.Entities.geral.permissao
{
    public class GrupoPermissaoSubmoduloAcao : BaseEntity
    {
        public Guid GrupoId { get; set; }
        public GrupoPermissao Grupo { get; set; }
        public Guid SubmoduloId { get; set; }
        public Submodulo Submodulo { get; set; }
        public Guid AcaoId { get; set; }
        public Acao Acao { get; set; }
        public bool Permitido { get; set; }
    }
}


