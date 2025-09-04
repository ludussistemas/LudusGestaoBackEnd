namespace LudusGestao.Domain.Interfaces.Services.geral
{
    public interface IPermissionValidator
    {
        Task<bool> HasPermissionAsync(Guid userId, Guid filialId, string permission);
        Task<bool> HasModuleAccessAsync(Guid userId, Guid filialId, string module);
    }
}
