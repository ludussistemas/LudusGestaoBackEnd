using LudusGestao.Core.Interfaces.Controllers;
using LudusGestao.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace LudusGestao.Core.Services
{
    public class DefaultValidationService<TDto, TCreateDto, TUpdateDto> : IValidationService<TDto, TCreateDto, TUpdateDto>
    {
        public virtual Task<IActionResult> ValidateAsync(TDto dto)
        {
            // Implementação padrão - sempre retorna null (válido)
            return Task.FromResult<IActionResult>(null);
        }

        public virtual Task<IActionResult> ValidateIdAsync(object id)
        {
            // Validação padrão para ID
            if (id == null)
            {
                return Task.FromResult<IActionResult>(new BadRequestObjectResult(new ApiResponse<object>(default)
                {
                    Success = false,
                    Message = "ID não pode ser nulo"
                }));
            }

            if (id is Guid guid && guid == Guid.Empty)
            {
                return Task.FromResult<IActionResult>(new BadRequestObjectResult(new ApiResponse<object>(default)
                {
                    Success = false,
                    Message = "ID não pode ser vazio"
                }));
            }

            return Task.FromResult<IActionResult>(null);
        }

        public virtual Task<IActionResult> ValidateCreateAsync(TCreateDto dto)
        {
            // Validação padrão para criação
            if (dto == null)
            {
                return Task.FromResult<IActionResult>(new BadRequestObjectResult(new ApiResponse<object>(default)
                {
                    Success = false,
                    Message = "Dados de criação não podem ser nulos"
                }));
            }

            return Task.FromResult<IActionResult>(null);
        }

        public virtual Task<IActionResult> ValidateUpdateAsync(object id, TUpdateDto dto)
        {
            // Validação padrão para atualização
            if (dto == null)
            {
                return Task.FromResult<IActionResult>(new BadRequestObjectResult(new ApiResponse<object>(default)
                {
                    Success = false,
                    Message = "Dados de atualização não podem ser nulos"
                }));
            }

            // Reutiliza a validação de ID
            return ValidateIdAsync(id);
        }
    }
}
