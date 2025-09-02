using Microsoft.AspNetCore.Mvc;

namespace LudusGestao.Core.Interfaces.Controllers
{
    public interface IAuthorizationService
    {
        Task<bool> CanReadAsync();
        Task<bool> CanCreateAsync();
        Task<bool> CanUpdateAsync(object id);
        Task<bool> CanDeleteAsync(object id);
        Task<IActionResult> GetUnauthorizedResponseAsync();
    }
}
