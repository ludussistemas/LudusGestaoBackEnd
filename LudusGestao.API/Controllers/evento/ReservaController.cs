using LudusGestao.Domain.DTOs.reserva.Reservas;
using LudusGestao.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LudusGestao.API.Controllers.eventos;

[ApiController]
[Route("api/reservas")]
[Authorize]
public class ReservasController : BaseCrudController<LudusGestao.Core.Interfaces.Services.IBaseCrudService<ReservaDTO, CreateReservaDTO, UpdateReservaDTO>, ReservaDTO, CreateReservaDTO, UpdateReservaDTO>
{
    public ReservasController(LudusGestao.Core.Interfaces.Services.IBaseCrudService<ReservaDTO, CreateReservaDTO, UpdateReservaDTO> service) : base(service) { }
}
