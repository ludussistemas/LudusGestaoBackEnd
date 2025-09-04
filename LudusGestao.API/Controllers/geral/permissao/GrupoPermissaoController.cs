using LudusGestao.Domain.DTOs.geral.GrupoPermissao;
using LudusGestao.Application.Services.geral.permissao;
using LudusGestao.Core.Controllers;
using LudusGestao.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LudusGestao.API.Controllers.geral.permissao
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GrupoPermissaoController : BaseCrudController<GrupoPermissaoService, GrupoPermissaoDTO, CreateGrupoPermissaoDTO, UpdateGrupoPermissaoDTO>
    {
        public GrupoPermissaoController(GrupoPermissaoService service) 
            : base(service)
        {
        }
    }
}
