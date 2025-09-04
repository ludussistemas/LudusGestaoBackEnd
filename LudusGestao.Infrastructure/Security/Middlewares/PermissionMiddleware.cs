using LudusGestao.Domain.Interfaces.Services;
using LudusGestao.Domain.Interfaces.Services.geral;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using LudusGestao.Infrastructure.Data.Context;

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
            var permissaoAcessoService = serviceProvider.GetRequiredService<LudusGestao.Domain.Interfaces.Services.geral.permissao.IPermissaoAcessoService>();
            var filialService = serviceProvider.GetRequiredService<LudusGestao.Domain.Interfaces.Services.infra.IFilialService>();
            var db = serviceProvider.GetRequiredService<ApplicationDbContext>();

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

            // Extrair filial do header (obrigatório)
            var filialId = ExtractFilial(context.Request);
            var isGet = string.Equals(context.Request.Method, "GET", StringComparison.OrdinalIgnoreCase);
            if (!filialId.HasValue && isGet)
            {
                await errorBuilder.BuildErrorResponseAsync(context, 400, "É necessário informar a filial para realizar esta operação. Você pode enviar a filial através do cabeçalho 'Filial' ou como parâmetro de consulta 'filial'/'filialId' na URL.");
                return;
            }

            // Propagar a filial para o contexto de request (filtro global e repositórios)
            if (filialId.HasValue)
            {
                filialService.SetFilialId(filialId.Value);
            }

            // Validar se o usuário possui acesso à filial informada
            var possuiAcessoAFilial = await permissaoAcessoService.UsuarioTemAcessoAFilial(usuarioId.Value, filialId.Value);
            if (!possuiAcessoAFilial)
            {
                await errorBuilder.BuildErrorResponseAsync(context, 403, "Usuário não possui acesso à filial informada.");
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

            // Extrair módulo, submodulo e ação da permissão
            string acao = permissaoNecessaria;
            string modulo = "";
            string submodulo = "";
            var parts = permissaoNecessaria?.Split('.') ?? Array.Empty<string>();
            if (parts.Length == 3)
            {
                modulo = parts[0];
                submodulo = parts[1];
                acao = parts[2];
            }

            // Verificar se o usuário tem a permissão necessária
            var temPermissao = filialId.HasValue
                ? await validator.HasPermissionAsync(usuarioId.Value, modulo, submodulo, acao, filialId)
                : true;
            if (!temPermissao)
            {
                string? filialNome = null;
                if (filialId.HasValue)
                {
                    filialNome = await db.Filiais
                        .Where(f => f.Id == filialId.Value)
                        .Select(f => f.Nome)
                        .FirstOrDefaultAsync();
                }

                var mensagem = filialId.HasValue
                    ? $"Você não tem permissão para {acao.ToLower()} {submodulo} no módulo {modulo} na filial {filialNome ?? filialId.Value.ToString()}. Caso precise deste acesso, contate o administrador."
                    : $"Você não tem permissão para {acao.ToLower()} {submodulo} no módulo {modulo}. Caso precise deste acesso, contate o administrador.";

                await errorBuilder.BuildErrorResponseAsync(context, 403, mensagem);
                return;
            }

            // Verificar acesso ao módulo pai se necessário
            var moduloPai = routeMapper.GetParentModule(path);
            if (!string.IsNullOrEmpty(moduloPai))
            {
                var temAcessoModulo = filialId.HasValue
                    ? await validator.HasModuleAccessAsync(usuarioId.Value, moduloPai, filialId)
                    : true;
                if (!temAcessoModulo)
                {
                    string? filialNome = null;
                    if (filialId.HasValue)
                    {
                        filialNome = await db.Filiais
                            .Where(f => f.Id == filialId.Value)
                            .Select(f => f.Nome)
                            .FirstOrDefaultAsync();
                    }

                    var mensagemModulo = filialId.HasValue
                        ? $"Você não tem permissão para acessar o módulo {moduloPai} na filial {filialNome ?? filialId.Value.ToString()}. Caso precise deste acesso, contate o administrador."
                        : $"Você não tem permissão para acessar o módulo {moduloPai}. Caso precise deste acesso, contate o administrador.";

                    await errorBuilder.BuildErrorResponseAsync(context, 403, mensagemModulo);
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
            var userIdClaim = user.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier ||
                c.Type == "sub" ||
                c.Type == "UserId");

            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return userId;
            }
            return null;
        }

        private Guid? ExtractFilial(HttpRequest request)
        {
            // Tentar extrair do header obrigatório "Filial"
            if (request.Headers.TryGetValue("Filial", out var filialHeader))
            {
                if (Guid.TryParse(filialHeader.ToString(), out var filialId))
                {
                    return filialId;
                }
            }

            // Alternativa: querystring ?filial=...
            var filialQuery = request.Query["filial"].FirstOrDefault()
                ?? request.Query["Filial"].FirstOrDefault()
                ?? request.Query["filialId"].FirstOrDefault()
                ?? request.Query["FilialId"].FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(filialQuery) && Guid.TryParse(filialQuery, out var filialIdFromQuery))
            {
                return filialIdFromQuery;
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
