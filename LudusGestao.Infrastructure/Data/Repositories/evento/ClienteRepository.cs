// Arquivo criado para padronização da estrutura de repositórios 
using LudusGestao.Core.Interfaces.Repositories.Base;
using LudusGestao.Domain.Entities.eventos;
using LudusGestao.Domain.Interfaces.Repositories.eventos;
using LudusGestao.Domain.Interfaces.Services;
using LudusGestao.Infrastructure.Data.Context;
using LudusGestao.Infrastructure.Data.Repositories.Base;
using LudusGestao.Infrastructure.Data.Repositories.Base.Filters;

namespace LudusGestao.Infrastructure.Data.Repositories.eventos;

public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
{
    public ClienteRepository(
        ApplicationDbContext context,
        ITenantService tenantService,
        ITenantFilter<Cliente> tenantFilter,
        IQuerySorter<Cliente> querySorter,
        IEnumerable<IFilterStrategy> filterStrategies)
        : base(context, tenantService, tenantFilter, querySorter, filterStrategies) { }
}
