using Microsoft.AspNetCore.Mvc;

namespace LudusGestao.Core.Services
{
    public class CustomValidationService<TDto, TCreateDto, TUpdateDto> : DefaultValidationService<TDto, TCreateDto, TUpdateDto>
    {
        public override async Task<IActionResult> ValidateCreateAsync(TCreateDto dto)
        {
            // Primeiro executa a validação base
            var baseValidation = await base.ValidateCreateAsync(dto);
            if (baseValidation != null) return baseValidation;

            // Aqui você pode adicionar validações customizadas específicas
            // Por exemplo, validar campos obrigatórios, formatos, etc.

            return null; // Retorna null se tudo estiver válido
        }

        public override async Task<IActionResult> ValidateUpdateAsync(object id, TUpdateDto dto)
        {
            // Primeiro executa a validação base
            var baseValidation = await base.ValidateUpdateAsync(id, dto);
            if (baseValidation != null) return baseValidation;

            // Aqui você pode adicionar validações customizadas específicas
            // Por exemplo, validar se o registro existe, se pode ser alterado, etc.

            return null; // Retorna null se tudo estiver válido
        }
    }
}
