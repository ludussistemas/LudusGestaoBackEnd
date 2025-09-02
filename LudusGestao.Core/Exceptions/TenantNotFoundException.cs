namespace LudusGestao.Core.Exceptions
{
    public class TenantNotFoundException : DomainException
    {
        public TenantNotFoundException(int tenantId) : base($"Tenant {tenantId} não encontrado") { }
    }
}
