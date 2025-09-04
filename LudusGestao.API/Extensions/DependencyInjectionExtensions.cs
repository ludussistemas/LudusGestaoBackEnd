using LudusGestao.Domain.Interfaces.Services.geral;
using LudusGestao.Application.Services;
using LudusGestao.Core.Interfaces.Repositories.Base;
using LudusGestao.Domain.Interfaces.Repositories;
using LudusGestao.Domain.Interfaces.Repositories.eventos;
using LudusGestao.Domain.Interfaces.Repositories.geral;
using LudusGestao.Domain.Interfaces.Repositories.geral.permissao;
using LudusGestao.Domain.Interfaces.Services;
using LudusGestao.Domain.Interfaces.Services.geral;
using LudusGestao.Domain.Interfaces.Services.geral.permissao;
using LudusGestao.Domain.Interfaces.Services.infra;
using LudusGestao.Infrastructure.Data.Repositories.Base;
using LudusGestao.Infrastructure.Data.Repositories.Base.Filters;
using LudusGestao.Infrastructure.Data.Repositories.eventos;
using LudusGestao.Infrastructure.Data.Repositories.geral;
using LudusGestao.Infrastructure.Security;
using LudusGestao.Infrastructure.Security.Handlers;
using LudusGestao.Infrastructure.Security.Services;

namespace LudusGestao.API.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddAppCoreServices(this IServiceCollection services)
        {
            // Tenant e contexto HTTP
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<LudusGestao.Domain.Interfaces.Services.infra.IFilialService, CurrentFilialService>();

            // Seed e HttpClient
            services.AddScoped<LudusGestao.Domain.Interfaces.Services.infra.ISeedService, LudusGestao.Infrastructure.Data.Seed.SeedService>();
            services.AddHttpClient();

            // Estratégias de filtro
            services.AddScoped<IFilterStrategy, JsonFilterStrategy>();
            services.AddScoped<IFilterStrategy, TextFilterStrategy>();
            services.AddScoped<IFilterStrategy, SimpleFilterStrategy>();

            // Filtros e ordenadores
            services.AddScoped(typeof(ITenantFilter<>), typeof(TenantFilter<>));
            services.AddScoped(typeof(LudusGestao.Infrastructure.Data.Repositories.Base.FilialFilter<>));
            services.AddScoped(typeof(IQuerySorter<>), typeof(QuerySorter<>));

            // Serviços de permissões
            services.AddScoped<IPermissionRouteMapper, PermissionRouteMapper>();
            services.AddScoped<IPermissionValidator, PermissionValidator>();
            services.AddScoped<IErrorResponseBuilder, ErrorResponseBuilder>();

            // Serviços de criação (multitenant bootstrap)
            services.AddScoped<ITenantCreationService, TenantCreationService>();
            services.AddScoped<ICompanyCreationService, CompanyCreationService>();
            services.AddScoped<IBranchCreationService, BranchCreationService>();
            services.AddScoped<IAdminUserCreationService, AdminUserCreationService>();

            // Repositórios base
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            // Repositórios e serviços específicos
            services.AddScoped<IReservaRepository, ReservaRepository>();
            services.AddScoped<ReservaService>();
            services.AddScoped<LudusGestao.Core.Interfaces.Services.IBaseCrudService<LudusGestao.Domain.DTOs.reserva.Reservas.ReservaDTO, LudusGestao.Domain.DTOs.reserva.Reservas.CreateReservaDTO, LudusGestao.Domain.DTOs.reserva.Reservas.UpdateReservaDTO>, ReservaService>();

            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<ClienteService>();
            services.AddScoped<LudusGestao.Core.Interfaces.Services.IBaseCrudService<LudusGestao.Domain.DTOs.reserva.Cliente.ClienteDTO, LudusGestao.Domain.DTOs.reserva.Cliente.CreateClienteDTO, LudusGestao.Domain.DTOs.reserva.Cliente.UpdateClienteDTO>, ClienteService>();

            services.AddScoped<IFilialRepository, FilialRepository>();
            services.AddScoped<CurrentFilialService>();
            services.AddScoped<LudusGestao.Core.Interfaces.Services.IBaseCrudService<LudusGestao.Domain.DTOs.Filial.FilialDTO, LudusGestao.Domain.DTOs.Filial.CreateFilialDTO, LudusGestao.Domain.DTOs.Filial.UpdateFilialDTO>, FilialService>();

            services.AddScoped<ILocalRepository, LocalRepository>();
            services.AddScoped<LocalService>();
            services.AddScoped<LudusGestao.Core.Interfaces.Services.IBaseCrudService<LudusGestao.Domain.DTOs.evento.Local.LocalDTO, LudusGestao.Domain.DTOs.evento.Local.CreateLocalDTO, LudusGestao.Domain.DTOs.evento.Local.UpdateLocalDTO>, LocalService>();

            services.AddScoped<IRecebivelRepository, RecebivelRepository>();
            services.AddScoped<RecebivelService>();
            services.AddScoped<LudusGestao.Core.Interfaces.Services.IBaseCrudService<LudusGestao.Domain.DTOs.evento.Recebivel.RecebivelDTO, LudusGestao.Domain.DTOs.evento.Recebivel.CreateRecebivelDTO, LudusGestao.Domain.DTOs.evento.Recebivel.UpdateRecebivelDTO>, RecebivelService>();

            // Serviços de permissões refatorados
            services.AddScoped<IPermissaoAcessoService, LudusGestao.Application.Services.geral.permissao.PermissaoAcessoService>();

            // Handlers de exceção
            services.AddScoped<IExceptionHandler, UnauthorizedExceptionHandler>();
            // services.AddScoped<IExceptionHandler, ValidationExceptionHandler>();
            // services.AddScoped<IExceptionHandler, NotFoundExceptionHandler>();
            services.AddScoped<IExceptionHandler, DefaultExceptionHandler>();

            // Repositórios e serviços gerais
            services.AddScoped<IEmpresaRepository, EmpresaRepository>();
            services.AddScoped<EmpresaService>();
            services.AddScoped<LudusGestao.Core.Interfaces.Services.IBaseCrudService<LudusGestao.Domain.DTOs.Empresa.EmpresaDTO, LudusGestao.Domain.DTOs.Empresa.CreateEmpresaDTO, LudusGestao.Domain.DTOs.Empresa.UpdateEmpresaDTO>, EmpresaService>();

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<UsuarioService>();
            services.AddScoped<LudusGestao.Core.Interfaces.Services.IBaseCrudService<LudusGestao.Domain.DTOs.Usuario.UsuarioDTO, LudusGestao.Domain.DTOs.Usuario.CreateUsuarioDTO, LudusGestao.Domain.DTOs.Usuario.UpdateUsuarioDTO>, UsuarioService>();

            services.AddScoped<IGrupoPermissaoRepository, LudusGestao.Infrastructure.Data.Repositories.geral.permissao.GrupoPermissaoRepository>();
            services.AddScoped<LudusGestao.Application.Services.geral.permissao.GrupoPermissaoService>();
            services.AddScoped<LudusGestao.Core.Interfaces.Services.IBaseCrudService<LudusGestao.Domain.DTOs.geral.GrupoPermissao.GrupoPermissaoDTO, LudusGestao.Domain.DTOs.geral.GrupoPermissao.CreateGrupoPermissaoDTO, LudusGestao.Domain.DTOs.geral.GrupoPermissao.UpdateGrupoPermissaoDTO>, LudusGestao.Application.Services.geral.permissao.GrupoPermissaoService>();

            // Novos repositórios de permissões
            services.AddScoped<IModuloRepository, LudusGestao.Infrastructure.Data.Repositories.geral.permissao.ModuloRepository>();
            services.AddScoped<ISubmoduloRepository, LudusGestao.Infrastructure.Data.Repositories.geral.permissao.SubmoduloRepository>();
            services.AddScoped<IAcaoRepository, LudusGestao.Infrastructure.Data.Repositories.geral.permissao.AcaoRepository>();
            services.AddScoped<IGrupoPermissaoModuloAcaoRepository, LudusGestao.Infrastructure.Data.Repositories.geral.permissao.GrupoPermissaoModuloAcaoRepository>();
            services.AddScoped<IGrupoPermissaoSubmoduloAcaoRepository, LudusGestao.Infrastructure.Data.Repositories.geral.permissao.GrupoPermissaoSubmoduloAcaoRepository>();
            services.AddScoped<IUsuarioFilialGrupoRepository, LudusGestao.Infrastructure.Data.Repositories.geral.permissao.UsuarioFilialGrupoRepository>();

            // Serviços de autenticação
            services.AddScoped<LudusGestao.Domain.Interfaces.Services.autenticacao.IAuthService, LudusGestao.Infrastructure.Security.AuthService>();
            services.AddScoped<LudusGestao.Domain.Interfaces.Services.geral.IAuthService, LudusGestao.Application.Services.AuthService>();

            // Serviços de gerenciamento
            services.AddScoped<IGerencialmentoService, GerencialmentoService>();

            return services;
        }
    }
}


