using Microsoft.AspNetCore.Http;
using LudusGestao.Domain.Interfaces.Services;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace LudusGestao.Infrastructure.Security.Middlewares
{
    public class PermissionMiddleware
    {
        private readonly RequestDelegate _next;

        public PermissionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            var path = context.Request.Path.Value?.ToLower() ?? "";

            // Ignorar rotas que não precisam de verificação de permissões
            if (DeveIgnorarVerificacao(path))
            {
                await _next(context);
                return;
            }

            // Obter serviços necessários
            var routeMapper = serviceProvider.GetRequiredService<IPermissionRouteMapper>();
            var validator = serviceProvider.GetRequiredService<IPermissionValidator>();
            var errorBuilder = serviceProvider.GetRequiredService<IErrorResponseBuilder>();

            // Verificar se o usuário está autenticado
            if (!context.User.Identity?.IsAuthenticated ?? true)
            {
                await errorBuilder.BuildErrorResponseAsync(context, 401, "Usuário não autenticado");
                return;
            }

            // Extrair informações do usuário do token
            var usuarioId = ExtractUserId(context.User);
            if (!usuarioId.HasValue)
            {
                await errorBuilder.BuildErrorResponseAsync(context, 401, "ID do usuário não encontrado no token");
                return;
            }

            // Determinar a permissão necessária baseada na rota
            var permissaoNecessaria = routeMapper.GetRequiredPermission(context.Request.Method, path);
            if (string.IsNullOrEmpty(permissaoNecessaria))
            {
                // Se não conseguiu determinar a permissão, permite o acesso (pode ser uma rota pública)
                await _next(context);
                return;
            }

            // Verificar se o usuário tem a permissão necessária
            var temPermissao = await validator.HasPermissionAsync(usuarioId.Value, permissaoNecessaria);
            if (!temPermissao)
            {
                await errorBuilder.BuildErrorResponseAsync(context, 403, $"Acesso negado. Permissão necessária: {permissaoNecessaria}");
                return;
            }

            // Verificar acesso ao módulo pai se necessário
            var moduloPai = routeMapper.GetParentModule(path);
            if (!string.IsNullOrEmpty(moduloPai))
            {
                var temAcessoModulo = await validator.HasModuleAccessAsync(usuarioId.Value, moduloPai);
                if (!temAcessoModulo)
                {
                    await errorBuilder.BuildErrorResponseAsync(context, 403, $"Acesso negado ao módulo: {moduloPai}");
                    return;
                }
            }

            await _next(context);
        }

        private bool DeveIgnorarVerificacao(string path)
        {
            return path == "/" ||
                   path.StartsWith("/swagger") ||
                   path.StartsWith("/health") ||
                   path == "/favicon.ico" ||
                   path.StartsWith("/api/autenticacao/") ||
                   path.StartsWith("/api/auth/") ||
                   path.StartsWith("/api/utilitarios/");
        }

        private Guid? ExtractUserId(ClaimsPrincipal user)
        {
            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == "UserId" || c.Type == "sub");
            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return userId;
            }
            return null;
        }
    }

    // Método de extensão para registrar o middleware corretamente
    public static class PermissionMiddlewareExtensions
    {
        public static IApplicationBuilder UsePermissionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PermissionMiddleware>();
        }
    }
}
