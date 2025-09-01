// Arquivo criado para padronização da estrutura de repositórios 
using LudusGestao.Domain.Entities;
using LudusGestao.Domain.Interfaces.Repositories;
using LudusGestao.Domain.Interfaces.Services;
using LudusGestao.Infrastructure.Data.Context;
using LudusGestao.Infrastructure.Data.Repositories.Base;
using LudusGestao.Domain.Interfaces.Repositories.Base;
using LudusGestao.Infrastructure.Data.Repositories.Base.Filters;
using Microsoft.EntityFrameworkCore;

namespace LudusGestao.Infrastructure.Data.Repositories.geral;

public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(
        ApplicationDbContext context, 
        ITenantService tenantService,
        ITenantFilter<Usuario> tenantFilter,
        IQuerySorter<Usuario> querySorter,
        IEnumerable<IFilterStrategy> filterStrategies) 
        : base(context, tenantService, tenantFilter, querySorter, filterStrategies) { }

    public async Task<Usuario?> ObterPorEmail(string email)
    {
        var query = _context.Set<Usuario>().AsQueryable();
        query = ApplyTenantFilter(query);
        return await query.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<Usuario?> ObterPorEmailGlobal(string email)
    {
        return await _context.Set<Usuario>()
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<IEnumerable<Usuario>> ObterPorEmpresa(Guid empresaId)
    {
        var query = _context.Set<Usuario>().AsQueryable();
        query = ApplyTenantFilter(query);
        return await query.Where(u => u.EmpresaId == empresaId).ToListAsync();
    }

    public async Task<IEnumerable<Usuario>> ObterPorGrupo(Guid grupoId)
    {
        var query = _context.Set<Usuario>().AsQueryable();
        query = ApplyTenantFilter(query);
        return await query.Where(u => u.GrupoPermissaoId == grupoId).ToListAsync();
    }

    public async Task<Usuario?> GetByEmailAsync(string email)
    {
        return await _context.Set<Usuario>()
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(u => u.Email == email);
    }
} 