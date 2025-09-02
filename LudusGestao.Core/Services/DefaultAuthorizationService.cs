using LudusGestao.Core.Interfaces.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace LudusGestao.Core.Services
{
    public class DefaultAuthorizationService : IAuthorizationService
    {
        public virtual Task<bool> CanReadAsync()
        {
            // Implementação padrão - sempre permite leitura
            return Task.FromResult(true);
        }

        public virtual Task<bool> CanCreateAsync()
        {
            // Implementação padrão - sempre permite criação
            return Task.FromResult(true);
        }

        public virtual Task<bool> CanUpdateAsync(object id)
        {
            // Implementação padrão - sempre permite atualização
            return Task.FromResult(true);
        }

        public virtual Task<bool> CanDeleteAsync(object id)
        {
            // Implementação padrão - sempre permite exclusão
            return Task.FromResult(true);
        }

        public virtual Task<IActionResult> GetUnauthorizedResponseAsync()
        {
            // Implementação padrão - retorna 403 Forbidden
            return Task.FromResult<IActionResult>(new ForbidResult());
        }
    }
}
