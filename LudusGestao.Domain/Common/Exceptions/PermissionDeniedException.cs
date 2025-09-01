using System;

namespace LudusGestao.Domain.Common.Exceptions
{
    public class PermissionDeniedException : DomainException
    {
        public PermissionDeniedException(string permission) : base($"Permissão negada: {permission}") { }
    }
}
