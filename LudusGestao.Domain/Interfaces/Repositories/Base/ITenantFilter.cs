using System.Linq;

namespace LudusGestao.Domain.Interfaces.Repositories.Base
{
    public interface ITenantFilter<T>
    {
        IQueryable<T> Apply(IQueryable<T> query, int tenantId);
    }
}
