using AutoMapper;
using LudusGestao.Domain.DTOs.Filial;
using LudusGestao.Core.Interfaces.Infrastructure;
using LudusGestao.Core.Interfaces.Repositories.Base;
using LudusGestao.Core.Interfaces.Services;
using LudusGestao.Core.Services;
using LudusGestao.Domain.Entities.geral;

namespace LudusGestao.Application.Services
{
    public class FilialService : BaseCrudService<Filial, FilialDTO, CreateFilialDTO, UpdateFilialDTO>, IBaseCrudService<FilialDTO, CreateFilialDTO, UpdateFilialDTO>
    {
        public FilialService(IBaseRepository<Filial> repository, IMapper mapper, IUnitOfWork unitOfWork) : base(repository, mapper, unitOfWork) { }

        // Adicione aqui apenas métodos customizados que não sejam CRUD padrão
    }
}
