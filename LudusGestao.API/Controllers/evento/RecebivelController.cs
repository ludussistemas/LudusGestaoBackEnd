using LudusGestao.Application.DTOs.evento.Recebivel;
using LudusGestao.Application.DTOs.infra.Utilitarios;
using LudusGestao.Core.Controllers;
using LudusGestao.Core.Models;
using LudusGestao.Domain.Enums.eventos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LudusGestao.API.Controllers.eventos;

[ApiController]
[Route("api/recebiveis")]
[Authorize]
public class RecebiveisController : BaseCrudController<LudusGestao.Core.Interfaces.Services.IBaseCrudService<RecebivelDTO, CreateRecebivelDTO, UpdateRecebivelDTO>, RecebivelDTO, CreateRecebivelDTO, UpdateRecebivelDTO>
{
    public RecebiveisController(LudusGestao.Core.Interfaces.Services.IBaseCrudService<RecebivelDTO, CreateRecebivelDTO, UpdateRecebivelDTO> service) : base(service) { }

    [HttpGet("resumo")]
    public async Task<IActionResult> ObterResumo()
    {
        try
        {
            var recebiveis = await _service.Listar();

            var resumo = new RecebivelResumoDTO
            {
                TotalRecebiveis = recebiveis.Count(),
                ValorTotal = recebiveis.Sum(r => r.Valor),
                ValorPendente = recebiveis.Where(r => r.Situacao == SituacaoRecebivel.Aberto).Sum(r => r.Valor),
                ValorPago = recebiveis.Where(r => r.Situacao == SituacaoRecebivel.Pago).Sum(r => r.Valor),
                ValorVencido = recebiveis.Where(r => r.Situacao == SituacaoRecebivel.Vencido).Sum(r => r.Valor),
                Pendentes = recebiveis.Count(r => r.Situacao == SituacaoRecebivel.Aberto),
                Pagos = recebiveis.Count(r => r.Situacao == SituacaoRecebivel.Pago),
                Vencidos = recebiveis.Count(r => r.Situacao == SituacaoRecebivel.Vencido)
            };

            return Ok(new ApiResponse<RecebivelResumoDTO>(resumo, "Resumo de recebíveis obtido com sucesso"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse<object>(default) { Success = false, Message = "Erro ao obter resumo de recebíveis" });
        }
    }
}