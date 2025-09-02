using LudusGestao.Core.Entities.Base;

namespace LudusGestao.Domain.Entities.geral
{
    public class GrupoPermissaoFilial : BaseEntity, ITenantEntity
    {
        public Guid GrupoPermissaoId { get; set; }
        public GrupoPermissao GrupoPermissao { get; set; }
        public Guid FilialId { get; set; }
        public Filial Filial { get; set; }
        public List<Guid> Permissoes { get; set; } = new List<Guid>();
        public int TenantId { get; set; }
    }
}
