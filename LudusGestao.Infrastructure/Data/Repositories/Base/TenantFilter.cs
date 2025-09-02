using LudusGestao.Core.Entities.Base;
using LudusGestao.Core.Interfaces.Repositories.Base;
using System.Linq.Expressions;

namespace LudusGestao.Infrastructure.Data.Repositories.Base
{
    public class TenantFilter<T> : ITenantFilter<T>
    {
        public IQueryable<T> Apply(IQueryable<T> query, int tenantId)
        {
            if (typeof(ITenantEntity).IsAssignableFrom(typeof(T)))
            {
                try
                {
                    var parameter = Expression.Parameter(typeof(T), "x");
                    var property = Expression.Property(parameter, "TenantId");
                    var constant = Expression.Constant(tenantId);
                    var condition = Expression.Equal(property, constant);
                    var lambda = Expression.Lambda<Func<T, bool>>(condition, parameter);
                    return query.Where(lambda);
                }
                catch
                {
                    return query.Where(x => false);
                }
            }
            return query;
        }
    }
}
