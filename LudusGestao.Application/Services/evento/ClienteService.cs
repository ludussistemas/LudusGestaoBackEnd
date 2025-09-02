using AutoMapper;
using LudusGestao.Domain.Entities.eventos;
using LudusGestao.Domain.Interfaces.Repositories.Base;
using LudusGestao.Domain.Interfaces.Repositories.eventos;
using LudusGestao.Application.Common.Interfaces;
using LudusGestao.Application.Common.Services;
using LudusGestao.Domain.Common;
using LudusGestao.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LudusGestao.Application.DTOs.reserva.Cliente;

namespace LudusGestao.Application.Services
{
    public class ClienteService : BaseCrudService<Cliente, ClienteDTO, CreateClienteDTO, UpdateClienteDTO>, IBaseCrudService<ClienteDTO, CreateClienteDTO, UpdateClienteDTO>
    {
        public ClienteService(IBaseRepository<Cliente> repository, IMapper mapper) : base(repository, mapper) { }

        // Adicione aqui apenas métodos customizados que não sejam CRUD padrão
    }
} 