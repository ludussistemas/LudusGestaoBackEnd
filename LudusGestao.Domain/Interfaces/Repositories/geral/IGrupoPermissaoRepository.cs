using LudusGestao.Domain.Entities.geral;
using LudusGestao.Domain.Interfaces.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LudusGestao.Domain.Interfaces.Repositories.geral
{
    public interface IGrupoPermissaoRepository : IBaseRepository<GrupoPermissao>
    {
        Task<IEnumerable<GrupoPermissao>> ObterComUsuarios();
        Task<GrupoPermissao?> ObterComUsuariosPorId(Guid id);
        Task<IEnumerable<Usuario>> ObterUsuariosDoGrupoAsync(Guid grupoId);
        Task AdicionarUsuarioAoGrupoAsync(Guid grupoId, Guid usuarioId);
        Task RemoverUsuarioDoGrupoAsync(Guid grupoId, Guid usuarioId);
    }
} 