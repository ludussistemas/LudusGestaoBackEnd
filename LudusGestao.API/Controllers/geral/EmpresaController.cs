using LudusGestao.Domain.DTOs.Empresa;
using LudusGestao.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LudusGestao.API.Controllers.geral;

[ApiController]
[Route("api/empresas")]
[Authorize]
public class EmpresasController : BaseCrudController<LudusGestao.Core.Interfaces.Services.IBaseCrudService<EmpresaDTO, CreateEmpresaDTO, UpdateEmpresaDTO>, EmpresaDTO, CreateEmpresaDTO, UpdateEmpresaDTO>
{
    public EmpresasController(LudusGestao.Core.Interfaces.Services.IBaseCrudService<EmpresaDTO, CreateEmpresaDTO, UpdateEmpresaDTO> service) : base(service) { }
}
