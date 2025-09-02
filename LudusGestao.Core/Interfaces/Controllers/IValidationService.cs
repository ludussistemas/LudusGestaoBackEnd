using Microsoft.AspNetCore.Mvc;

namespace LudusGestao.Core.Interfaces.Controllers
{
    public interface IValidationService<TDto>
    {
        Task<IActionResult> ValidateAsync(TDto dto);
        Task<IActionResult> ValidateIdAsync(object id);
    }

    public interface IValidationService<TDto, TCreateDto, TUpdateDto> : IValidationService<TDto>
    {
        Task<IActionResult> ValidateCreateAsync(TCreateDto dto);
        Task<IActionResult> ValidateUpdateAsync(object id, TUpdateDto dto);
    }
}
