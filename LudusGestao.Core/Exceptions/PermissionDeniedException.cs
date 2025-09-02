namespace LudusGestao.Core.Exceptions
{
    public class PermissionDeniedException : DomainException
    {
        public PermissionDeniedException(string permission) : base($"Permissão negada: {permission}") { }
    }
}
