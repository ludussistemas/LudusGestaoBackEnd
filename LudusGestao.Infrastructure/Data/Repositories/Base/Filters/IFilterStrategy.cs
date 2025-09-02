namespace LudusGestao.Infrastructure.Data.Repositories.Base.Filters
{
    public interface IFilterStrategy
    {
        bool CanHandle(string filter);
        IQueryable<T> Apply<T>(IQueryable<T> query, string filter);
    }
}
