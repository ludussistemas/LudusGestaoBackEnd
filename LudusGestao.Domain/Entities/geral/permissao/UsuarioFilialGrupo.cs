using LudusGestao.Core.Entities.Base;

namespace LudusGestao.Domain.Entities.geral.permissao
{
    public class UsuarioFilialGrupo : BaseEntity
    {
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public Guid FilialId { get; set; }
        public Filial Filial { get; set; }
        public Guid GrupoId { get; set; }
        public GrupoPermissao Grupo { get; set; }
    }
}


