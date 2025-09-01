using LudusGestao.Domain.Interfaces.Services;

namespace LudusGestao.Infrastructure.Security.Services
{
    public class PermissionRouteMapper : IPermissionRouteMapper
    {
        public string GetRequiredPermission(string method, string path)
        {
            if (path.StartsWith("/api/empresas"))
            {
                return method switch
                {
                    "GET" => "empresa.visualizar",
                    "POST" => "empresa.criar",
                    "PUT" => "empresa.editar",
                    "DELETE" => "empresa.excluir",
                    _ => null
                };
            }

            if (path.StartsWith("/api/filiais"))
            {
                return method switch
                {
                    "GET" => "filial.visualizar",
                    "POST" => "filial.criar",
                    "PUT" => "filial.editar",
                    "DELETE" => "filial.excluir",
                    _ => null
                };
            }

            if (path.StartsWith("/api/usuarios"))
            {
                return method switch
                {
                    "GET" => "usuario.visualizar",
                    "POST" => "usuario.criar",
                    "PUT" => "usuario.editar",
                    "DELETE" => "usuario.excluir",
                    _ => null
                };
            }

            if (path.StartsWith("/api/grupos-permissoes"))
            {
                return method switch
                {
                    "GET" => "grupo.visualizar",
                    "POST" => "grupo.criar",
                    "PUT" => "grupo.editar",
                    "DELETE" => "grupo.excluir",
                    _ => null
                };
            }

            if (path.StartsWith("/api/clientes"))
            {
                return method switch
                {
                    "GET" => "cliente.visualizar",
                    "POST" => "cliente.criar",
                    "PUT" => "cliente.editar",
                    "DELETE" => "cliente.excluir",
                    _ => null
                };
            }

            if (path.StartsWith("/api/locais"))
            {
                return method switch
                {
                    "GET" => "local.visualizar",
                    "POST" => "local.criar",
                    "PUT" => "local.editar",
                    "DELETE" => "local.excluir",
                    _ => null
                };
            }

            if (path.StartsWith("/api/reservas"))
            {
                return method switch
                {
                    "GET" => "reserva.visualizar",
                    "POST" => "reserva.criar",
                    "PUT" => "reserva.editar",
                    "DELETE" => "reserva.excluir",
                    _ => null
                };
            }

            if (path.StartsWith("/api/recebiveis"))
            {
                return method switch
                {
                    "GET" => "recebivel.visualizar",
                    "POST" => "recebivel.criar",
                    "PUT" => "recebivel.editar",
                    "DELETE" => "recebivel.excluir",
                    _ => null
                };
            }

            return null;
        }

        public string GetParentModule(string path)
        {
            if (path.StartsWith("/api/empresas") || path.StartsWith("/api/filiais") || 
                path.StartsWith("/api/usuarios") || path.StartsWith("/api/grupos-permissoes"))
            {
                return "configuracoes";
            }

            if (path.StartsWith("/api/clientes") || path.StartsWith("/api/locais") || 
                path.StartsWith("/api/reservas") || path.StartsWith("/api/recebiveis"))
            {
                return "reservas";
            }

            return null;
        }
    }
}
