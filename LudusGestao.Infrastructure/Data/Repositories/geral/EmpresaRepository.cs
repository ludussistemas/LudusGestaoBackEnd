// Arquivo criado para padronização da estrutura de repositórios 
using LudusGestao.Domain.Entities;
using LudusGestao.Domain.Interfaces.Repositories;
using LudusGestao.Domain.Interfaces.Services;
using LudusGestao.Infrastructure.Data.Context;
using LudusGestao.Infrastructure.Data.Repositories.Base;
using LudusGestao.Domain.Interfaces.Repositories.Base;
using LudusGestao.Infrastructure.Data.Repositories.Base.Filters;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LudusGestao.Domain.Interfaces.Repositories.geral;
using LudusGestao.Domain.Entities.geral;

namespace LudusGestao.Infrastructure.Data.Repositories.geral;

public class EmpresaRepository : BaseRepository<Empresa>, IEmpresaRepository
{
    public EmpresaRepository(
        ApplicationDbContext context, 
        ITenantService tenantService,
        ITenantFilter<Empresa> tenantFilter,
        IQuerySorter<Empresa> querySorter,
        IEnumerable<IFilterStrategy> filterStrategies) 
        : base(context, tenantService, tenantFilter, querySorter, filterStrategies) { }

    public async Task<int> GetMaxTenantIdAsync()
    {
        return await _context.Empresas.MaxAsync(e => (int?)e.TenantId) ?? 0;
    }
} 