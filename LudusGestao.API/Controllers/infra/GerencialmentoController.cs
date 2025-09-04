using LudusGestao.Domain.Interfaces.Services.geral;
using LudusGestao.Domain.DTOs.Gerencialmento;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LudusGestao.API.Controllers.infra
{
    [ApiController]
    [Route("api/gerencialmento")]
    [Authorize(Policy = "TenantMaster")]
    public class GerencialController : ControllerBase
    {
        private readonly IGerencialmentoService _gerencialmentoService;

        public GerencialController(IGerencialmentoService gerencialmentoService)
        {
            _gerencialmentoService = gerencialmentoService;
        }

        [HttpPost("novo-cliente")]
        public async Task<IActionResult> CriarNovoCliente([FromBody] CriarNovoClienteDTO dto)
        {
            var resultado = await _gerencialmentoService.CriarNovoCliente(dto);
            if (resultado.Success)
                return Ok(resultado);
            return BadRequest(resultado);
        }

        [HttpPost("alterar-senha")]
        public async Task<IActionResult> AlterarSenha([FromBody] AlterarSenhaDTO dto)
        {
            var resultado = await _gerencialmentoService.AlterarSenha(dto);
            if (resultado.Success)
                return Ok(resultado);
            return BadRequest(resultado);
        }
    }
}
