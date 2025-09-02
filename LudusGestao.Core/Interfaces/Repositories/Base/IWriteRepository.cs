namespace LudusGestao.Core.Interfaces.Repositories.Base
{
    public interface IWriteRepository<T> where T : class
    {
        Task Criar(T entity);
        Task Atualizar(T entity);
        Task Remover(Guid id);
    }
}
