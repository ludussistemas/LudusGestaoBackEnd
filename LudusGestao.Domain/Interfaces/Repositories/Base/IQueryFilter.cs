namespace LudusGestao.Domain.Interfaces.Repositories.Base
{
    public interface IQueryFilter<T>
    {
        IQueryable<T> Apply(IQueryable<T> query, string filter);
    }
}
