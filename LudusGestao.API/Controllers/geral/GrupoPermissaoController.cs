using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LudusGestao.Application.Services;
using LudusGestao.Application.Common.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using LudusGestao.Application.DTOs.geral.GrupoPermissao;

namespace LudusGestao.API.Controllers.geral;

[ApiController]
[Route("api/grupos-permissoes")]
[Authorize]
public class GruposPermissoesController : BaseCrudController<GrupoPermissaoService, GrupoPermissaoDTO, CreateGrupoPermissaoDTO, UpdateGrupoPermissaoDTO>
{
    private readonly GrupoPermissaoService _grupoService;

    public GruposPermissoesController(GrupoPermissaoService service) : base(service) 
    {
        _grupoService = service;
    }

    [HttpGet("{id}/usuarios")]
    public async Task<IActionResult> ObterUsuariosDoGrupo(Guid id)
    {
        try
        {
            var usuarios = await _grupoService.ObterUsuariosDoGrupoAsync(id);
            return Ok(new ApiResponse<IEnumerable<object>>(usuarios, "Usuários do grupo obtidos com sucesso"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse<object>(default) { Success = false, Message = "Erro ao obter usuários do grupo" });
        }
    }

    [HttpPost("{id}/usuarios")]
    public async Task<IActionResult> AdicionarUsuarioAoGrupo(Guid id, [FromBody] AdicionarUsuarioAoGrupoDTO dto)
    {
        try
        {
            await _grupoService.AdicionarUsuarioAoGrupoAsync(id, dto.UsuarioId);
            return Ok(new ApiResponse<object>(default, "Usuário adicionado ao grupo com sucesso"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse<object>(default) { Success = false, Message = "Erro ao adicionar usuário ao grupo" });
        }
    }

    [HttpDelete("{id}/usuarios/{usuarioId}")]
    public async Task<IActionResult> RemoverUsuarioDoGrupo(Guid id, Guid usuarioId)
    {
        try
        {
            await _grupoService.RemoverUsuarioDoGrupoAsync(id, usuarioId);
            return Ok(new ApiResponse<object>(default, "Usuário removido do grupo com sucesso"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse<object>(default) { Success = false, Message = "Erro ao remover usuário do grupo" });
        }
    }
}

public class AdicionarUsuarioAoGrupoDTO
{
    public Guid UsuarioId { get; set; }
} 