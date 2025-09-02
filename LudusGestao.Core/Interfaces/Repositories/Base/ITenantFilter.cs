namespace LudusGestao.Core.Interfaces.Repositories.Base
{
    public interface ITenantFilter<T>
    {
        IQueryable<T> Apply(IQueryable<T> query, int tenantId);
    }
}
