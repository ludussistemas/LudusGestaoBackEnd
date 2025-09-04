using LudusGestao.Core.Entities.Base;
using LudusGestao.Core.Exceptions;
using LudusGestao.Core.Interfaces.Infrastructure;
using LudusGestao.Domain.Entities.eventos;
using LudusGestao.Domain.Entities.geral;
using LudusGestao.Domain.Entities.geral.permissao;
using LudusGestao.Domain.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace LudusGestao.Infrastructure.Data.Context
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        private readonly ITenantService _tenantService;

        // Construtor para runtime (injeção de dependência)
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ITenantService tenantService) : base(options)
        {
            _tenantService = tenantService;
        }

        // Construtor para tempo de design (migrations, update-database)
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            // _tenantService permanece nulo
        }

        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Filial> Filiais { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Local> Locais { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Recebivel> Recebiveis { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<GrupoPermissao> GruposPermissoes { get; set; }
        public DbSet<Modulo> Modulos { get; set; }
        public DbSet<Submodulo> Submodulos { get; set; }
        public DbSet<Acao> Acoes { get; set; }
        public DbSet<GrupoPermissaoModuloAcao> GruposPermissoesModulosAcoes { get; set; }
        public DbSet<GrupoPermissaoSubmoduloAcao> GruposPermissoesSubmodulosAcoes { get; set; }
        public DbSet<UsuarioFilialGrupo> UsuariosFiliaisGrupos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Filtro global de tenant
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ITenantEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var method = typeof(ApplicationDbContext).GetMethod(nameof(SetTenantFilter), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                        ?.MakeGenericMethod(entityType.ClrType);
                    method?.Invoke(this, new object[] { modelBuilder });
                }
            }




            // Configuração para List<string> Comodidades
            modelBuilder.Entity<Local>()
                .Property(l => l.Comodidades)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                )
                .Metadata.SetValueComparer(new ValueComparer<List<string>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()));



            // Configuração para propriedades opcionais
            modelBuilder.Entity<Usuario>()
                .Property(u => u.Foto)
                .IsRequired(false);

            modelBuilder.Entity<Cliente>()
                .Property(c => c.Observacoes)
                .IsRequired(false);

            modelBuilder.Entity<Cliente>()
                .Property(c => c.DataAtualizacao)
                .IsRequired(false);

            modelBuilder.Entity<Reserva>()
                .Property(r => r.Observacoes)
                .IsRequired(false);

            modelBuilder.Entity<Local>()
                .Property(l => l.Descricao)
                .IsRequired(false);

            modelBuilder.Entity<Local>()
                .Property(l => l.Capacidade)
                .IsRequired(false);



            modelBuilder.Entity<Recebivel>()
                .Property(r => r.ReservaId)
                .IsRequired(false);
        }

        private void SetTenantFilter<TEntity>(ModelBuilder modelBuilder) where TEntity : class, ITenantEntity
        {
            // Só aplica o filtro se o serviço estiver disponível (runtime)
            if (_tenantService != null)
                modelBuilder.Entity<TEntity>().HasQueryFilter(e => e.TenantId == _tenantService.GetTenantId());
        }

        // Removido o método Seed e toda a sua lógica conforme solicitado

        public override int SaveChanges()
        {
            SetAndValidateTenantIds();
            SetTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetAndValidateTenantIds();
            SetTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        // Métodos de seed preservados, se necessários, podem chamar base.SaveChangesAsync diretamente
        public async Task<int> SaveChangesWithoutTenantValidationAsync(CancellationToken cancellationToken = default) => await base.SaveChangesAsync(cancellationToken);
        public async Task<int> SaveChangesForSeedAsync(CancellationToken cancellationToken = default) => await base.SaveChangesAsync(cancellationToken);

        private void SetAndValidateTenantIds()
        {
            if (_tenantService == null) return;
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);
            foreach (var entry in entries)
            {
                if (entry.Entity is ITenantEntity tenantEntity)
                {
                    // Se o TenantId já está definido (como no seed), não altera
                    if (tenantEntity.TenantId == 0)
                    {
                        // Tenta atribuir o TenantId do contexto
                        var currentTenantId = _tenantService.GetTenantId();
                        if (currentTenantId == 0)
                            throw new ValidationException("TenantId não pode ser 0. Operação não permitida.");
                        tenantEntity.TenantId = currentTenantId;
                    }
                    // Remove a validação que impedia TenantId = 0, pois no seed definimos manualmente
                }
            }
        }

        private void SetTimestamps()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.Entity is BaseEntity baseEntity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        baseEntity.DataCriacao = DateTime.UtcNow;
                        baseEntity.DataAtualizacao = null;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        baseEntity.DataAtualizacao = DateTime.UtcNow;
                    }
                }
            }
        }

        public async Task CommitAsync()
        {
            await SaveChangesAsync();
        }
    }
}
