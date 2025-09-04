using LudusGestao.Core.Interfaces.Repositories.Base;
using LudusGestao.Domain.Entities.geral.permissao;

namespace LudusGestao.Domain.Interfaces.Repositories.geral.permissao
{
    public interface IAcaoRepository : IBaseRepository<Acao>
    {
        Task<Acao?> ObterPorNomeAsync(string nome);
    }
}


