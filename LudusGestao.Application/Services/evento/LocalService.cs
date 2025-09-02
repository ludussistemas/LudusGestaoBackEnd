using AutoMapper;
using LudusGestao.Application.DTOs.evento.Local;
using LudusGestao.Core.Interfaces.Infrastructure;
using LudusGestao.Core.Interfaces.Repositories.Base;
using LudusGestao.Core.Interfaces.Services;
using LudusGestao.Core.Services;
using LudusGestao.Domain.Entities.eventos;

namespace LudusGestao.Application.Services
{
    public class LocalService : BaseCrudService<Local, LocalDTO, CreateLocalDTO, UpdateLocalDTO>, IBaseCrudService<LocalDTO, CreateLocalDTO, UpdateLocalDTO>
    {
        public LocalService(IBaseRepository<Local> repository, IMapper mapper, IUnitOfWork unitOfWork) : base(repository, mapper, unitOfWork) { }

        // Adicione aqui apenas métodos customizados que não sejam CRUD padrão
    }
}