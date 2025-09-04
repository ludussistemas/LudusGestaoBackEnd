using LudusGestao.Domain.DTOs.geral.permissao;
using LudusGestao.Core.Controllers;
using LudusGestao.Domain.Interfaces.Services.geral.permissao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LudusGestao.API.Controllers.geral.permissao
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsuarioPermissaoController : ControllerBase
    {
        private readonly IPermissaoAcessoService _permissaoAcessoService;

        public UsuarioPermissaoController(IPermissaoAcessoService permissaoAcessoService)
        {
            _permissaoAcessoService = permissaoAcessoService;
        }

        [HttpGet("menu")]
        public async Task<ActionResult<MenuDto>> MontarMenu([FromQuery] Guid usuarioId, [FromQuery] Guid filialId)
        {
            var menu = await _permissaoAcessoService.MontarMenu(usuarioId, filialId);
            return Ok((MenuDto)menu);
        }

        [HttpGet("tem-permissao-modulo")]
        public async Task<ActionResult<bool>> TemPermissaoModulo(
            [FromQuery] Guid usuarioId, 
            [FromQuery] Guid filialId, 
            [FromQuery] string moduloNome, 
            [FromQuery] string acaoNome)
        {
            var temPermissao = await _permissaoAcessoService.TemPermissaoModulo(usuarioId, filialId, moduloNome, acaoNome);
            return Ok(temPermissao);
        }

        [HttpGet("tem-permissao-submodulo")]
        public async Task<ActionResult<bool>> TemPermissaoSubmodulo(
            [FromQuery] Guid usuarioId, 
            [FromQuery] Guid filialId, 
            [FromQuery] string submoduloNome, 
            [FromQuery] string acaoNome)
        {
            var temPermissao = await _permissaoAcessoService.TemPermissaoSubmodulo(usuarioId, filialId, submoduloNome, acaoNome);
            return Ok(temPermissao);
        }
    }
}
