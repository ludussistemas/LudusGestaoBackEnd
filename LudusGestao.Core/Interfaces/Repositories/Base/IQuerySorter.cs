namespace LudusGestao.Core.Interfaces.Repositories.Base
{
    public interface IQuerySorter<T>
    {
        IQueryable<T> Apply(IQueryable<T> query, string sort);
    }
}
