using System;
using System.Threading.Tasks;

namespace LudusGestao.Domain.Interfaces.Services.geral
{
    public interface IPermissionValidator
    {
        Task<bool> HasPermissionAsync(Guid userId, string permission);
        Task<bool> HasModuleAccessAsync(Guid userId, string module);
    }
}
