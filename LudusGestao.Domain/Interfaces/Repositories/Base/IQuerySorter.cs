using System.Linq;

namespace LudusGestao.Domain.Interfaces.Repositories.Base
{
    public interface IQuerySorter<T>
    {
        IQueryable<T> Apply(IQueryable<T> query, string sort);
    }
}
