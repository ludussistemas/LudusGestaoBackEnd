using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LudusGestao.Application.Services;
using LudusGestao.Application.DTOs.reserva.Reservas;

namespace LudusGestao.API.Controllers.eventos;

[ApiController]
[Route("api/reservas")]
[Authorize]
public class ReservasController : BaseCrudController<ReservaService, ReservaDTO, CreateReservaDTO, UpdateReservaDTO>
{
    public ReservasController(ReservaService service) : base(service) { }
} 