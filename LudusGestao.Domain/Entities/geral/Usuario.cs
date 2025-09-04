using LudusGestao.Core.Entities.Base;
using LudusGestao.Domain.Entities.geral.permissao;
using LudusGestao.Domain.Enums.geral;

namespace LudusGestao.Domain.Entities.geral
{
    public class Usuario : BaseEntity, ITenantEntity
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Cargo { get; set; }
        public Guid EmpresaId { get; set; }
        public Empresa Empresa { get; set; } = null!;
        public Guid? GrupoPermissaoId { get; set; }
        public GrupoPermissao? GrupoPermissao { get; set; }
        public SituacaoBase Situacao { get; set; }
        public DateTime UltimoAcesso { get; set; }
        public string? Foto { get; set; }
        public string Senha { get; set; }
        public int TenantId { get; set; }
    }
}
