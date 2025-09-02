namespace LudusGestao.Core.Interfaces.Infrastructure
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
