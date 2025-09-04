namespace LudusGestao.Domain.Interfaces.Services.geral
{
    public interface IPermissionValidator
    {
        Task<bool> HasPermissionAsync(Guid userId, string modulo, string submodulo, string acao, Guid? filialId = null);
        Task<bool> HasModuleAccessAsync(Guid userId, string modulo, Guid? filialId = null);
    }
}
