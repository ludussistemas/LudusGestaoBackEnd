using LudusGestao.Core.Interfaces.Repositories.Base;
using LudusGestao.Domain.Entities.geral;

namespace LudusGestao.Domain.Interfaces.Repositories.geral
{
    public interface IPermissaoRepository : IBaseRepository<Permissao>
    {
        Task<IEnumerable<Permissao>> ObterPorModuloPai(string moduloPai);
        Task<IEnumerable<Permissao>> ObterPorSubmodulo(string submodulo);
        Task<IEnumerable<string>> ObterModulosPai();
        Task<IEnumerable<string>> ObterSubmodulos();
        Task<IEnumerable<Permissao>> ObterPorIdsAsync(IEnumerable<Guid> ids);
        Task<IEnumerable<Permissao>> ObterPermissoesPorGrupoAsync(Guid grupoId);
    }
}
