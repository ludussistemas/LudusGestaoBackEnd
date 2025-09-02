using LudusGestao.Application.DTOs.infra.Utilitarios;
using LudusGestao.Application.DTOs.reserva.Cliente;
using LudusGestao.Core.Controllers;
using LudusGestao.Core.Models;
using LudusGestao.Domain.Enums.eventos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LudusGestao.API.Controllers.eventos;

[ApiController]
[Route("api/clientes")]
[Authorize]
public class ClientesController : BaseCrudController<LudusGestao.Core.Interfaces.Services.IBaseCrudService<ClienteDTO, CreateClienteDTO, UpdateClienteDTO>, ClienteDTO, CreateClienteDTO, UpdateClienteDTO>
{
    public ClientesController(LudusGestao.Core.Interfaces.Services.IBaseCrudService<ClienteDTO, CreateClienteDTO, UpdateClienteDTO> service) : base(service) { }

    [HttpGet("resumo")]
    public async Task<IActionResult> ObterResumo()
    {
        try
        {
            var clientes = await _service.Listar();
            var dataAtual = DateTime.UtcNow;

            var resumo = new ClienteResumoDTO
            {
                TotalClientes = clientes.Count(),
                Ativos = clientes.Count(c => c.Situacao == SituacaoCliente.Ativo.ToString()),
                Inativos = clientes.Count(c => c.Situacao == SituacaoCliente.Inativo.ToString()),
                NovosMes = clientes.Count(c =>
                    c.DataCriacao.Year == dataAtual.Year &&
                    c.DataCriacao.Month == dataAtual.Month),
                PessoaJuridica = clientes.Count(c => c.Documento.Length > 11)
            };

            return Ok(new ApiResponse<ClienteResumoDTO>(resumo, "Resumo de clientes obtido com sucesso"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse<object>(default) { Success = false, Message = "Erro ao obter resumo de clientes" });
        }
    }
}