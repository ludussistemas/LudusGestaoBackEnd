using AutoMapper;
using LudusGestao.Domain.DTOs.Usuario;
using LudusGestao.Core.Interfaces.Infrastructure;
using LudusGestao.Core.Interfaces.Services;
using LudusGestao.Core.Services;
using LudusGestao.Domain.Entities.geral;
using LudusGestao.Domain.Interfaces.Repositories.geral;

namespace LudusGestao.Application.Services
{
    public class UsuarioService : BaseCrudService<Usuario, UsuarioDTO, CreateUsuarioDTO, UpdateUsuarioDTO>, IBaseCrudService<UsuarioDTO, CreateUsuarioDTO, UpdateUsuarioDTO>
    {
        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper, IUnitOfWork unitOfWork) : base(usuarioRepository, mapper, unitOfWork) { }
    }
}
