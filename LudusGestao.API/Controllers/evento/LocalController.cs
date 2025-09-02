using LudusGestao.Application.DTOs.evento.Local;
using LudusGestao.Application.DTOs.infra.Utilitarios;
using LudusGestao.Core.Controllers;
using LudusGestao.Core.Models;
using LudusGestao.Domain.Enums.eventos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LudusGestao.API.Controllers.eventos;

[ApiController]
[Route("api/locais")]
[Authorize]
public class LocaisController : BaseCrudController<LudusGestao.Core.Interfaces.Services.IBaseCrudService<LocalDTO, CreateLocalDTO, UpdateLocalDTO>, LocalDTO, CreateLocalDTO, UpdateLocalDTO>
{
    public LocaisController(LudusGestao.Core.Interfaces.Services.IBaseCrudService<LocalDTO, CreateLocalDTO, UpdateLocalDTO> service) : base(service) { }

    [HttpGet("resumo")]
    public async Task<IActionResult> ObterResumo()
    {
        try
        {
            var locais = await _service.Listar();

            var resumo = new LocalResumoDTO
            {
                TotalLocais = locais.Count(),
                Ativos = locais.Count(l => l.Situacao == SituacaoLocal.Ativo),
                Inativos = locais.Count(l => l.Situacao == SituacaoLocal.Inativo),
                Manutencao = locais.Count(l => l.Situacao == SituacaoLocal.Manutencao),
                ValorMedioHora = locais.Any() ? locais.Average(l => l.ValorHora) : 0
            };

            return Ok(new ApiResponse<LocalResumoDTO>(resumo, "Resumo de locais obtido com sucesso"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse<object>(default) { Success = false, Message = "Erro ao obter resumo de locais" });
        }
    }
}