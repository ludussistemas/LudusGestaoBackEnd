using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LudusGestao.Application.DTOs.Usuario;
using LudusGestao.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LudusGestao.Domain.Interfaces.Services.geral;

namespace LudusGestao.API.Controllers.geral
{
    [ApiController]
    [Route("api/usuarios")]
    [Authorize]
    public class UsuarioPermissaoController : ControllerBase
    {
        private readonly IPermissaoVerificacaoService _permissaoService;

        public UsuarioPermissaoController(IPermissaoVerificacaoService permissaoService)
        {
            _permissaoService = permissaoService;
        }

        [HttpGet("{id}/permissoes")]
        public async Task<IActionResult> ObterPermissoesUsuario(Guid id)
        {
            try
            {
                var permissoes = await _permissaoService.ObterPermissoesUsuarioAsync(id);
                return Ok(new ApiResponse<IEnumerable<string>>(permissoes, "Permissões do usuário obtidas com sucesso"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>(default) { Success = false, Message = "Erro ao obter permissões do usuário" });
            }
        }

        [HttpGet("{id}/filiais")]
        public async Task<IActionResult> ObterFiliaisUsuario(Guid id)
        {
            try
            {
                var filiais = await _permissaoService.ObterFiliaisUsuarioAsync(id);
                return Ok(new ApiResponse<IEnumerable<Guid>>(filiais, "Filiais do usuário obtidas com sucesso"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>(default) { Success = false, Message = "Erro ao obter filiais do usuário" });
            }
        }

        [HttpGet("{id}/modulos")]
        public async Task<IActionResult> ObterModulosUsuario(Guid id)
        {
            try
            {
                var modulos = await _permissaoService.ObterModulosUsuarioAsync(id);
                return Ok(new ApiResponse<IEnumerable<string>>(modulos, "Módulos do usuário obtidos com sucesso"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>(default) { Success = false, Message = "Erro ao obter módulos do usuário" });
            }
        }
    }
}
