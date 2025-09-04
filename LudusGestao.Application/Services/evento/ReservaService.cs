using AutoMapper;
using LudusGestao.Domain.DTOs.reserva.Reservas;
using LudusGestao.Core.Interfaces.Infrastructure;
using LudusGestao.Core.Interfaces.Repositories.Base;
using LudusGestao.Core.Interfaces.Services;
using LudusGestao.Core.Services;
using LudusGestao.Domain.Entities.eventos;

namespace LudusGestao.Application.Services
{
    public class ReservaService : BaseCrudService<Reserva, ReservaDTO, CreateReservaDTO, UpdateReservaDTO>, IBaseCrudService<ReservaDTO, CreateReservaDTO, UpdateReservaDTO>
    {
        public ReservaService(IBaseRepository<Reserva> repository, IMapper mapper, IUnitOfWork unitOfWork) : base(repository, mapper, unitOfWork) { }

        // Adicione aqui apenas métodos customizados que não sejam CRUD padrão
    }
}
