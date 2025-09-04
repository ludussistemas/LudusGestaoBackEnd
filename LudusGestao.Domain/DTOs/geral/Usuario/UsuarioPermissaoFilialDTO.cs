namespace LudusGestao.Domain.DTOs.Usuario
{
    public class UsuarioPermissaoFilialDTO
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid FilialId { get; set; }
        public List<Guid> Permissoes { get; set; } = new List<Guid>();
        public int TenantId { get; set; }
    }

    public class CreateUsuarioPermissaoFilialDTO
    {
        public Guid UsuarioId { get; set; }
        public Guid FilialId { get; set; }
        public List<Guid> Permissoes { get; set; } = new List<Guid>();
    }

    public class UpdateUsuarioPermissaoFilialDTO
    {
        public List<Guid> Permissoes { get; set; } = new List<Guid>();
    }
}
