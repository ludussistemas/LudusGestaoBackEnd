using LudusGestao.Core.Interfaces.Repositories.Base;
using LudusGestao.Domain.Entities.eventos;

namespace LudusGestao.Domain.Interfaces.Repositories.eventos
{
    public interface IReservaRepository : IBaseRepository<Reserva>
    {
        Task<IEnumerable<Reserva>> GetReservasByClienteAsync(Guid clienteId);
    }
}
