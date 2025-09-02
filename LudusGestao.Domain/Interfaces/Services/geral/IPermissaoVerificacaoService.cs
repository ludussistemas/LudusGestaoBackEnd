using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LudusGestao.Domain.Interfaces.Services.geral
{
    public interface IPermissaoVerificacaoService
    {
        Task<bool> UsuarioTemPermissaoAsync(Guid usuarioId, string permissao, Guid? filialId = null);
        Task<bool> UsuarioTemAcessoModuloAsync(Guid usuarioId, string moduloPai);
        Task<bool> UsuarioTemAcessoFilialAsync(Guid usuarioId, Guid filialId);
        Task<IEnumerable<string>> ObterPermissoesUsuarioAsync(Guid usuarioId, Guid? filialId = null);
        Task<IEnumerable<Guid>> ObterFiliaisUsuarioAsync(Guid usuarioId);
        Task<IEnumerable<string>> ObterModulosUsuarioAsync(Guid usuarioId);
    }
}
