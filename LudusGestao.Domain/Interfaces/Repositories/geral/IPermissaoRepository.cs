using LudusGestao.Domain.Entities.geral;
using LudusGestao.Domain.Interfaces.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LudusGestao.Domain.Interfaces.Repositories.geral
{
    public interface IPermissaoRepository : IBaseRepository<Permissao>
    {
        Task<IEnumerable<Permissao>> ObterPorModuloPai(string moduloPai);
        Task<IEnumerable<Permissao>> ObterPorSubmodulo(string submodulo);
        Task<IEnumerable<string>> ObterModulosPai();
        Task<IEnumerable<string>> ObterSubmodulos();
        Task<IEnumerable<Permissao>> ObterPorIdsAsync(IEnumerable<Guid> ids);
    }
} 