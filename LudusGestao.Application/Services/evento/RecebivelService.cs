using AutoMapper;
using LudusGestao.Domain.DTOs.evento.Recebivel;
using LudusGestao.Core.Interfaces.Infrastructure;
using LudusGestao.Core.Interfaces.Repositories.Base;
using LudusGestao.Core.Interfaces.Services;
using LudusGestao.Core.Services;
using LudusGestao.Domain.Entities.eventos;

namespace LudusGestao.Application.Services
{
    public class RecebivelService : BaseCrudService<Recebivel, RecebivelDTO, CreateRecebivelDTO, UpdateRecebivelDTO>, IBaseCrudService<RecebivelDTO, CreateRecebivelDTO, UpdateRecebivelDTO>
    {
        public RecebivelService(IBaseRepository<Recebivel> repository, IMapper mapper, IUnitOfWork unitOfWork) : base(repository, mapper, unitOfWork) { }

        // Adicione aqui apenas métodos customizados que não sejam CRUD padrão
    }
}
