using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using LudusGestao.Infrastructure.Data.Context;
using LudusGestao.Application.Services;
using LudusGestao.Application.Common.Interfaces;
using LudusGestao.Infrastructure.Data.Repositories.geral;
using LudusGestao.Infrastructure.Data.Repositories.eventos;
using LudusGestao.Infrastructure.Data.Repositories.Base;
using LudusGestao.Infrastructure.Data.Repositories.Base.Filters;
using LudusGestao.Domain.Interfaces.Repositories;
using LudusGestao.Domain.Interfaces.Repositories.Base;
using LudusGestao.Application.Mappings;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using LudusGestao.Domain.Interfaces.Services;
using LudusGestao.Domain.Interfaces.Services.infra;
using LudusGestao.Infrastructure.Security;
using LudusGestao.Infrastructure.Security.Middlewares;
using LudusGestao.Infrastructure.Security.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutoMapper;
using FluentValidation.AspNetCore;
using LudusGestao.Domain.Entities;
using Microsoft.OpenApi.Models;
using LudusGestao.Domain.Common.Constants;
using LudusGestao.Domain.Interfaces.Repositories.geral;
using LudusGestao.Domain.Interfaces.Repositories.eventos;
using LudusGestao.Domain.Interfaces.Services.geral;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "LudusGestao API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Insira o token JWT no formato: Bearer {seu token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        RateLimitPartition.GetFixedWindowLimiter("global", _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = RateLimitConstants.PermitLimit,
            Window = TimeSpan.FromMinutes(RateLimitConstants.WindowMinutes)
        }));
});

// DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<MappingProfile>();

// TenantService e IHttpContextAccessor
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ITenantService, TenantService>();

// SeedService
builder.Services.AddScoped<LudusGestao.Domain.Interfaces.Services.infra.ISeedService, LudusGestao.Infrastructure.Data.Seed.SeedService>();

// HttpClient para APIs externas
builder.Services.AddHttpClient();

// Registrar estratégias de filtro
builder.Services.AddScoped<IFilterStrategy, JsonFilterStrategy>();
builder.Services.AddScoped<IFilterStrategy, TextFilterStrategy>();
builder.Services.AddScoped<IFilterStrategy, SimpleFilterStrategy>();

// Registrar filtros e ordenação
builder.Services.AddScoped(typeof(ITenantFilter<>), typeof(TenantFilter<>));
builder.Services.AddScoped(typeof(IQuerySorter<>), typeof(QuerySorter<>));

// Registrar serviços de permissões
builder.Services.AddScoped<IPermissionRouteMapper, PermissionRouteMapper>();
builder.Services.AddScoped<IPermissionValidator, PermissionValidator>();
builder.Services.AddScoped<IErrorResponseBuilder, ErrorResponseBuilder>();

// Registrar serviços de criação
builder.Services.AddScoped<ITenantCreationService, TenantCreationService>();
builder.Services.AddScoped<ICompanyCreationService, CompanyCreationService>();
builder.Services.AddScoped<IBranchCreationService, BranchCreationService>();
builder.Services.AddScoped<IAdminUserCreationService, AdminUserCreationService>();

// Repositórios e Services
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IReservaRepository, ReservaRepository>();
builder.Services.AddScoped<ReservaService>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<IFilialRepository, FilialRepository>();
builder.Services.AddScoped<FilialService>();
builder.Services.AddScoped<ILocalRepository, LocalRepository>();
builder.Services.AddScoped<LocalService>();
builder.Services.AddScoped<IRecebivelRepository, RecebivelRepository>();
builder.Services.AddScoped<RecebivelService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<UsuarioService>();
// Registrar AuthService da Infrastructure para a interface de domínio
builder.Services.AddScoped<LudusGestao.Domain.Interfaces.Services.autenticacao.IAuthService, LudusGestao.Infrastructure.Security.AuthService>();
// Registrar AuthService da Application para uso na controller
builder.Services.AddScoped<LudusGestao.Application.Services.AuthService>();
builder.Services.AddScoped<IEmpresaRepository, EmpresaRepository>();
builder.Services.AddScoped<EmpresaService>();
builder.Services.AddScoped<IGrupoPermissaoRepository, GrupoPermissaoRepository>();
builder.Services.AddScoped<GrupoPermissaoService>();
builder.Services.AddScoped<IPermissaoRepository, PermissaoRepository>();
builder.Services.AddScoped<PermissaoService>();

// Novos repositórios e serviços de permissões
builder.Services.AddScoped<IGrupoPermissaoFilialRepository, GrupoPermissaoFilialRepository>();
builder.Services.AddScoped<IUsuarioPermissaoFilialRepository, UsuarioPermissaoFilialRepository>();
builder.Services.AddScoped<IPermissaoVerificacaoService, PermissaoVerificacaoService>();

builder.Services.AddScoped<IGerencialmentoService, LudusGestao.Application.Services.GerencialService>();

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = JwtConstants.Issuer,
            ValidAudience = JwtConstants.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConstants.Key)),
            ClockSkew = TimeSpan.Zero
        };
        
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"JWT Authentication failed: {context.Exception.Message}");
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine($"JWT Token validated for user: {context.Principal?.Identity?.Name}");
                return Task.CompletedTask;
            }
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseAuthentication();
app.UseMiddleware<TenantMiddleware>();
app.UsePermissionMiddleware();
app.UseAuthorization();
app.UseRateLimiter();
app.MapControllers();
app.MapHealthChecks("/health");

// Bloco para rodar apenas o seed se argumento 'seed' for passado
if (args.Length > 0 && args[0].ToLower() == "seed")
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
    Console.WriteLine("Seed executado com sucesso.");
    return;
}

app.Run(); 