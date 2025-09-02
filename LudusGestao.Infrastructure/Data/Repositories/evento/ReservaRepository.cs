using LudusGestao.Core.Interfaces.Repositories.Base;
using LudusGestao.Domain.Entities.eventos;
using LudusGestao.Domain.Interfaces.Repositories.eventos;
using LudusGestao.Domain.Interfaces.Services;
using LudusGestao.Infrastructure.Data.Context;
using LudusGestao.Infrastructure.Data.Repositories.Base;
using LudusGestao.Infrastructure.Data.Repositories.Base.Filters;
using Microsoft.EntityFrameworkCore;

namespace LudusGestao.Infrastructure.Data.Repositories.eventos
{
    public class ReservaRepository : BaseRepository<Reserva>, IReservaRepository
    {
        public ReservaRepository(
        ApplicationDbContext context,
        ITenantService tenantService,
        ITenantFilter<Reserva> tenantFilter,
        IQuerySorter<Reserva> querySorter,
        IEnumerable<IFilterStrategy> filterStrategies)
        : base(context, tenantService, tenantFilter, querySorter, filterStrategies) { }

        public async Task<IEnumerable<Reserva>> GetReservasByClienteAsync(Guid clienteId)
        {
            var query = _context.Reservas.AsQueryable();
            query = ApplyTenantFilter(query);
            return await query.Where(r => r.ClienteId == clienteId).ToListAsync();
        }

        public async Task<IEnumerable<Reserva>> ListarPorTenant(int tenantId)
        {
            return await _context.Set<Reserva>().Where(r => r.TenantId == tenantId).ToListAsync();
        }
    }
}
