using LudusGestao.Core.Interfaces.Repositories.Base;
using LudusGestao.Domain.Entities.geral;
using LudusGestao.Domain.Interfaces.Repositories.geral;
using LudusGestao.Domain.Interfaces.Services;
using LudusGestao.Infrastructure.Data.Context;
using LudusGestao.Infrastructure.Data.Repositories.Base;
using LudusGestao.Infrastructure.Data.Repositories.Base.Filters;
using Microsoft.EntityFrameworkCore;

namespace LudusGestao.Infrastructure.Data.Repositories.geral
{
    public class GrupoPermissaoRepository : BaseRepository<GrupoPermissao>, IGrupoPermissaoRepository
    {
        public GrupoPermissaoRepository(
        ApplicationDbContext context,
        ITenantService tenantService,
        ITenantFilter<GrupoPermissao> tenantFilter,
        IQuerySorter<GrupoPermissao> querySorter,
        IEnumerable<IFilterStrategy> filterStrategies)
        : base(context, tenantService, tenantFilter, querySorter, filterStrategies)
        {
        }

        public async Task<IEnumerable<GrupoPermissao>> ObterComUsuarios()
        {
            var query = _context.Set<GrupoPermissao>().AsQueryable();
            query = ApplyTenantFilter(query);
            return await query.ToListAsync();
        }

        public async Task<GrupoPermissao?> ObterComUsuariosPorId(Guid id)
        {
            var query = _context.Set<GrupoPermissao>().AsQueryable();
            query = ApplyTenantFilter(query);
            return await query.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Usuario>> ObterUsuariosDoGrupoAsync(Guid grupoId)
        {
            var query = _context.Set<Usuario>().AsQueryable();
            return await query.Where(u => u.GrupoPermissaoId == grupoId).ToListAsync();
        }

        public async Task AdicionarUsuarioAoGrupoAsync(Guid grupoId, Guid usuarioId)
        {
            var usuario = await _context.Set<Usuario>().FirstOrDefaultAsync(u => u.Id == usuarioId);
            if (usuario != null)
            {
                usuario.GrupoPermissaoId = grupoId;
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoverUsuarioDoGrupoAsync(Guid grupoId, Guid usuarioId)
        {
            var usuario = await _context.Set<Usuario>().FirstOrDefaultAsync(u => u.Id == usuarioId && u.GrupoPermissaoId == grupoId);
            if (usuario != null)
            {
                usuario.GrupoPermissaoId = null;
                await _context.SaveChangesAsync();
            }
        }
    }
}
