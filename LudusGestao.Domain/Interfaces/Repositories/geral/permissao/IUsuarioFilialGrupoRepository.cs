using LudusGestao.Core.Interfaces.Repositories.Base;
using LudusGestao.Domain.Entities.geral.permissao;

namespace LudusGestao.Domain.Interfaces.Repositories.geral.permissao
{
    public interface IUsuarioFilialGrupoRepository : IBaseRepository<UsuarioFilialGrupo>
    {
        Task<List<Guid>> ObterGruposDoUsuarioNaFilialAsync(Guid usuarioId, Guid filialId);
    }
}


