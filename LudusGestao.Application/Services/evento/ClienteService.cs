using AutoMapper;
using LudusGestao.Application.DTOs.reserva.Cliente;
using LudusGestao.Core.Interfaces.Infrastructure;
using LudusGestao.Core.Interfaces.Repositories.Base;
using LudusGestao.Core.Interfaces.Services;
using LudusGestao.Core.Services;
using LudusGestao.Domain.Entities.eventos;

namespace LudusGestao.Application.Services
{
    public class ClienteService : BaseCrudService<Cliente, ClienteDTO, CreateClienteDTO, UpdateClienteDTO>, IBaseCrudService<ClienteDTO, CreateClienteDTO, UpdateClienteDTO>
    {
        public ClienteService(IBaseRepository<Cliente> repository, IMapper mapper, IUnitOfWork unitOfWork) : base(repository, mapper, unitOfWork) { }

        // Adicione aqui apenas métodos customizados que não sejam CRUD padrão
    }
}