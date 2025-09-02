using FluentValidation;
using LudusGestao.API.Extensions;
using LudusGestao.Application.Mappings;
using LudusGestao.Core.Constants;
using LudusGestao.Core.Models;
using LudusGestao.Infrastructure.Data.Context;
using LudusGestao.Infrastructure.Security.Middlewares;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

builder.Services.AddAppCors();
builder.Services.AddAppAuthorization();
builder.Services.AddAppSwagger();
builder.Services.AddAppHealthChecks(builder.Configuration);
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
// Registrar UnitOfWork via ApplicationDbContext
builder.Services.AddScoped<LudusGestao.Core.Interfaces.Infrastructure.IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<MappingProfile>();

builder.Services.AddAppCoreServices();

// JWT Authentication
builder.Services.Configure<AuthOptions>(builder.Configuration.GetSection("Auth"));
builder.Services.AddAppJwt();

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