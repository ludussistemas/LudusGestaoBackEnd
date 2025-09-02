namespace LudusGestao.Domain.Interfaces.Services.geral
{
    public interface IPermissaoVerificacaoService
    {
        Task<bool> VerificarPermissaoUsuarioAsync(Guid usuarioId, string permissao, Guid? filialId = null);
        Task<IEnumerable<string>> ObterPermissoesUsuarioAsync(Guid usuarioId, Guid? filialId = null);
    }

    public interface IFilialAcessoService
    {
        Task<bool> VerificarAcessoFilialAsync(Guid usuarioId, Guid filialId);
        Task<IEnumerable<Guid>> ObterFiliaisUsuarioAsync(Guid usuarioId);
    }

    public interface IModuloAcessoService
    {
        Task<bool> VerificarAcessoModuloAsync(Guid usuarioId, string moduloPai);
        Task<IEnumerable<string>> ObterModulosUsuarioAsync(Guid usuarioId);
    }
}
