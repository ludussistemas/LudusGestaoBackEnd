using System;

namespace LudusGestao.Domain.Common.Exceptions
{
    public class TenantNotFoundException : DomainException
    {
        public TenantNotFoundException(int tenantId) : base($"Tenant {tenantId} não encontrado") { }
    }
}
