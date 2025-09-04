using LudusGestao.Domain.DTOs.Filial;
using LudusGestao.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LudusGestao.API.Controllers.geral;

[ApiController]
[Route("api/filiais")]
[Authorize]
public class FiliaisController : BaseCrudController<LudusGestao.Core.Interfaces.Services.IBaseCrudService<FilialDTO, CreateFilialDTO, UpdateFilialDTO>, FilialDTO, CreateFilialDTO, UpdateFilialDTO>
{
    public FiliaisController(LudusGestao.Core.Interfaces.Services.IBaseCrudService<FilialDTO, CreateFilialDTO, UpdateFilialDTO> service) : base(service) { }
}
