using AutoMapper;
using LudusGestao.Application.DTOs.Empresa;
using LudusGestao.Core.Interfaces.Infrastructure;
using LudusGestao.Core.Interfaces.Repositories.Base;
using LudusGestao.Core.Interfaces.Services;
using LudusGestao.Core.Services;
using LudusGestao.Domain.Entities.geral;

namespace LudusGestao.Application.Services
{
    public class EmpresaService : BaseCrudService<Empresa, EmpresaDTO, CreateEmpresaDTO, UpdateEmpresaDTO>, IBaseCrudService<EmpresaDTO, CreateEmpresaDTO, UpdateEmpresaDTO>
    {
        public EmpresaService(IBaseRepository<Empresa> repository, IMapper mapper, IUnitOfWork unitOfWork) : base(repository, mapper, unitOfWork) { }

        // Adicione aqui apenas métodos customizados que não sejam CRUD padrão
    }
}