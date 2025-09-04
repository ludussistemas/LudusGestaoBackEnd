using LudusGestao.Core.Interfaces.Repositories.Base;
using LudusGestao.Domain.Entities.geral.permissao;

namespace LudusGestao.Domain.Interfaces.Repositories.geral.permissao
{
    public interface ISubmoduloRepository : IBaseRepository<Submodulo>
    {
        Task<IEnumerable<Submodulo>> ObterPorModuloAsync(Guid moduloId);
    }
}


