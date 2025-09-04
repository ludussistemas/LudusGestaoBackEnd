namespace LudusGestao.Domain.Interfaces.Services.geral.permissao
{
    public interface IPermissaoAcessoService
    {
        Task<bool> TemPermissaoModulo(Guid usuarioId, Guid filialId, string moduloNome, string acaoNome);
        Task<bool> TemPermissaoSubmodulo(Guid usuarioId, Guid filialId, string submoduloNome, string acaoNome);
        Task<object> MontarMenu(Guid usuarioId, Guid filialId);
        Task<bool> UsuarioTemAcessoAFilial(Guid usuarioId, Guid filialId);
    }
}
