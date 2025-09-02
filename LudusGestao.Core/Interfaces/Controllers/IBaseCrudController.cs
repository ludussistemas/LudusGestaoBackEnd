using LudusGestao.Core.Common;
using LudusGestao.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LudusGestao.Core.Interfaces.Controllers
{
    public interface IBaseCrudController<TDto, TCreateDto, TUpdateDto>
    {
        Task<IActionResult> Listar([FromQuery] QueryParamsBase queryParams);
        Task<IActionResult> ObterPorId(Guid id);
        Task<IActionResult> Criar([FromBody] TCreateDto dto);
        Task<IActionResult> Atualizar(Guid id, [FromBody] TUpdateDto dto);
        Task<IActionResult> Remover(Guid id);
    }

    public interface IBaseCrudController<TDto, TCreateDto, TUpdateDto, TService> : IBaseCrudController<TDto, TCreateDto, TUpdateDto>
        where TService : IBaseCrudService<TDto, TCreateDto, TUpdateDto>
    {
        TService Service { get; }
    }
}
