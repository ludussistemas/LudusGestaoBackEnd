namespace LudusGestao.Core.Interfaces.Repositories.Base
{
    public interface IBaseRepository<T> : IReadRepository<T>, IWriteRepository<T> where T : class
    {
        // Interface composta que herda de IReadRepository e IWriteRepository
        // Permite que implementações específicas escolham quais operações suportar
    }
}
