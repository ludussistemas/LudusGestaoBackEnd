using LudusGestao.Core.Common;

namespace LudusGestao.Core.Interfaces.Repositories.Base
{
    public interface IReadRepository<T> where T : class
    {
        Task<T> ObterPorId(Guid id);
        Task<(IEnumerable<T> Items, int TotalCount)> ListarPaginado(QueryParamsBase queryParams);
        Task<IEnumerable<T>> Listar();
    }
}
