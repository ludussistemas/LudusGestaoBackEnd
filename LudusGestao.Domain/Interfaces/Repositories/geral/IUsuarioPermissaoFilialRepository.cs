using LudusGestao.Domain.Entities.geral;
using LudusGestao.Domain.Interfaces.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LudusGestao.Domain.Interfaces.Repositories.geral
{
    public interface IUsuarioPermissaoFilialRepository : IBaseRepository<UsuarioPermissaoFilial>
    {
        Task<UsuarioPermissaoFilial?> ObterPorUsuarioEFilialAsync(Guid usuarioId, Guid filialId);
        Task<IEnumerable<UsuarioPermissaoFilial>> ObterPorUsuarioAsync(Guid usuarioId);
        Task<IEnumerable<Guid>> ObterFiliaisPorUsuarioAsync(Guid usuarioId);
        Task<IEnumerable<UsuarioPermissaoFilial>> ObterPorFilialAsync(Guid filialId);
    }
} 