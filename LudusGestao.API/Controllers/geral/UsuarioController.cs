using LudusGestao.Domain.DTOs.Usuario;
using LudusGestao.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LudusGestao.API.Controllers.geral;

[ApiController]
[Route("api/usuarios")]
[Authorize]
public class UsuariosController : BaseCrudController<LudusGestao.Core.Interfaces.Services.IBaseCrudService<UsuarioDTO, CreateUsuarioDTO, UpdateUsuarioDTO>, UsuarioDTO, CreateUsuarioDTO, UpdateUsuarioDTO>
{
    public UsuariosController(LudusGestao.Core.Interfaces.Services.IBaseCrudService<UsuarioDTO, CreateUsuarioDTO, UpdateUsuarioDTO> service) : base(service) { }
}
