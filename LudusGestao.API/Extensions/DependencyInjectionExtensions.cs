using LudusGestao.Application.Common.Interfaces;
using LudusGestao.Application.Services;
using LudusGestao.Core.Interfaces.Repositories.Base;
using LudusGestao.Domain.Interfaces.Repositories;
using LudusGestao.Domain.Interfaces.Repositories.eventos;
using LudusGestao.Domain.Interfaces.Repositories.geral;
using LudusGestao.Domain.Interfaces.Services;
using LudusGestao.Domain.Interfaces.Services.geral;
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

            // Seed e HttpClient
            services.AddScoped<LudusGestao.Domain.Interfaces.Services.infra.ISeedService, LudusGestao.Infrastructure.Data.Seed.SeedService>();
            services.AddHttpClient();

            // Estratégias de filtro
            services.AddScoped<IFilterStrategy, JsonFilterStrategy>();
            services.AddScoped<IFilterStrategy, TextFilterStrategy>();
            services.AddScoped<IFilterStrategy, SimpleFilterStrategy>();

            // Filtros e ordenadores
            services.AddScoped(typeof(ITenantFilter<>), typeof(TenantFilter<>));
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
            services.AddScoped<LudusGestao.Core.Interfaces.Services.IBaseCrudService<LudusGestao.Application.DTOs.reserva.Reservas.ReservaDTO, LudusGestao.Application.DTOs.reserva.Reservas.CreateReservaDTO, LudusGestao.Application.DTOs.reserva.Reservas.UpdateReservaDTO>, ReservaService>();

            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<ClienteService>();
            services.AddScoped<LudusGestao.Core.Interfaces.Services.IBaseCrudService<LudusGestao.Application.DTOs.reserva.Cliente.ClienteDTO, LudusGestao.Application.DTOs.reserva.Cliente.CreateClienteDTO, LudusGestao.Application.DTOs.reserva.Cliente.UpdateClienteDTO>, ClienteService>();

            services.AddScoped<IFilialRepository, FilialRepository>();
            services.AddScoped<FilialService>();
            services.AddScoped<LudusGestao.Core.Interfaces.Services.IBaseCrudService<LudusGestao.Application.DTOs.Filial.FilialDTO, LudusGestao.Application.DTOs.Filial.CreateFilialDTO, LudusGestao.Application.DTOs.Filial.UpdateFilialDTO>, FilialService>();

            services.AddScoped<ILocalRepository, LocalRepository>();
            services.AddScoped<LocalService>();
            services.AddScoped<LudusGestao.Core.Interfaces.Services.IBaseCrudService<LudusGestao.Application.DTOs.evento.Local.LocalDTO, LudusGestao.Application.DTOs.evento.Local.CreateLocalDTO, LudusGestao.Application.DTOs.evento.Local.UpdateLocalDTO>, LocalService>();

            services.AddScoped<IRecebivelRepository, RecebivelRepository>();
            services.AddScoped<RecebivelService>();
            services.AddScoped<LudusGestao.Core.Interfaces.Services.IBaseCrudService<LudusGestao.Application.DTOs.evento.Recebivel.RecebivelDTO, LudusGestao.Application.DTOs.evento.Recebivel.CreateRecebivelDTO, LudusGestao.Application.DTOs.evento.Recebivel.UpdateRecebivelDTO>, RecebivelService>();

            // Serviços de permissões refatorados
            services.AddScoped<IPermissaoVerificacaoService, PermissaoVerificacaoService>();
            services.AddScoped<IFilialAcessoService, FilialAcessoService>();
            services.AddScoped<IModuloAcessoService, ModuloAcessoService>();

            // Handlers de exceção
            services.AddScoped<IExceptionHandler, UnauthorizedExceptionHandler>();
            // services.AddScoped<IExceptionHandler, ValidationExceptionHandler>();
            // services.AddScoped<IExceptionHandler, NotFoundExceptionHandler>();
            services.AddScoped<IExceptionHandler, DefaultExceptionHandler>();

            // Repositórios e serviços gerais
            services.AddScoped<IEmpresaRepository, EmpresaRepository>();
            services.AddScoped<EmpresaService>();
            services.AddScoped<LudusGestao.Core.Interfaces.Services.IBaseCrudService<LudusGestao.Application.DTOs.Empresa.EmpresaDTO, LudusGestao.Application.DTOs.Empresa.CreateEmpresaDTO, LudusGestao.Application.DTOs.Empresa.UpdateEmpresaDTO>, EmpresaService>();

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<UsuarioService>();
            services.AddScoped<LudusGestao.Core.Interfaces.Services.IBaseCrudService<LudusGestao.Application.DTOs.Usuario.UsuarioDTO, LudusGestao.Application.DTOs.Usuario.CreateUsuarioDTO, LudusGestao.Application.DTOs.Usuario.UpdateUsuarioDTO>, UsuarioService>();

            services.AddScoped<IPermissaoRepository, PermissaoRepository>();
            services.AddScoped<PermissaoService>();
            services.AddScoped<LudusGestao.Core.Interfaces.Services.IBaseCrudService<LudusGestao.Application.DTOs.geral.Permissao.PermissaoDTO, LudusGestao.Application.DTOs.geral.Permissao.CreatePermissaoDTO, LudusGestao.Application.DTOs.geral.Permissao.UpdatePermissaoDTO>, PermissaoService>();

            services.AddScoped<IGrupoPermissaoRepository, GrupoPermissaoRepository>();
            services.AddScoped<GrupoPermissaoService>();
            services.AddScoped<LudusGestao.Core.Interfaces.Services.IBaseCrudService<LudusGestao.Application.DTOs.geral.GrupoPermissao.GrupoPermissaoDTO, LudusGestao.Application.DTOs.geral.GrupoPermissao.CreateGrupoPermissaoDTO, LudusGestao.Application.DTOs.geral.GrupoPermissao.UpdateGrupoPermissaoDTO>, GrupoPermissaoService>();

            // Repositórios de permissões por filial
            services.AddScoped<IGrupoPermissaoFilialRepository, GrupoPermissaoFilialRepository>();
            services.AddScoped<IUsuarioPermissaoFilialRepository, UsuarioPermissaoFilialRepository>();

            // Serviços de autenticação
            services.AddScoped<LudusGestao.Domain.Interfaces.Services.autenticacao.IAuthService, LudusGestao.Infrastructure.Security.AuthService>();
            services.AddScoped<LudusGestao.Application.Common.Interfaces.IAuthService, LudusGestao.Application.Services.AuthService>();

            // Serviços de gerenciamento
            services.AddScoped<IGerencialmentoService, GerencialmentoService>();

            return services;
        }
    }
}


