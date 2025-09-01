namespace LudusGestao.Domain.Interfaces.Services
{
    public interface IPermissionRouteMapper
    {
        string GetRequiredPermission(string method, string path);
        string GetParentModule(string path);
    }
}
