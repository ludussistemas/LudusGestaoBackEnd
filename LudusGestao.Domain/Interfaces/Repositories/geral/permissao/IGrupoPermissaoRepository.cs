using LudusGestao.Core.Interfaces.Repositories.Base;
using LudusGestao.Domain.Entities.geral.permissao;
using LudusGestao.Domain.Enums.geral;

namespace LudusGestao.Domain.Interfaces.Repositories.geral.permissao
{
    public interface IGrupoPermissaoRepository : IBaseRepository<GrupoPermissao>
    {
        Task<IEnumerable<GrupoPermissao>> ObterPorNomeAsync(string nome);
        Task<IEnumerable<GrupoPermissao>> ObterPorSituacaoAsync(SituacaoBase situacao);
    }
}
