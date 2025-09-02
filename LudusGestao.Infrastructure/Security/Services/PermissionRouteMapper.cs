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
                    "GET" => "configuracoes.empresa.visualizar",
                    "POST" => "configuracoes.empresa.criar",
                    "PUT" => "configuracoes.empresa.editar",
                    "DELETE" => "configuracoes.empresa.excluir",
                    _ => null
                };
            }

            if (path.StartsWith("/api/filiais"))
            {
                return method switch
                {
                    "GET" => "configuracoes.filial.visualizar",
                    "POST" => "configuracoes.filial.criar",
                    "PUT" => "configuracoes.filial.editar",
                    "DELETE" => "configuracoes.filial.excluir",
                    _ => null
                };
            }

            if (path.StartsWith("/api/usuarios"))
            {
                return method switch
                {
                    "GET" => "configuracoes.usuario.visualizar",
                    "POST" => "configuracoes.usuario.criar",
                    "PUT" => "configuracoes.usuario.editar",
                    "DELETE" => "configuracoes.usuario.excluir",
                    _ => null
                };
            }

            if (path.StartsWith("/api/grupos-permissoes"))
            {
                return method switch
                {
                    "GET" => "configuracoes.grupo.visualizar",
                    "POST" => "configuracoes.grupo.criar",
                    "PUT" => "configuracoes.grupo.editar",
                    "DELETE" => "configuracoes.grupo.excluir",
                    _ => null
                };
            }

            if (path.StartsWith("/api/clientes"))
            {
                return method switch
                {
                    "GET" => "reservas.cliente.visualizar",
                    "POST" => "reservas.cliente.criar",
                    "PUT" => "reservas.cliente.editar",
                    "DELETE" => "reservas.cliente.excluir",
                    _ => null
                };
            }

            if (path.StartsWith("/api/locais"))
            {
                return method switch
                {
                    "GET" => "reservas.local.visualizar",
                    "POST" => "reservas.local.criar",
                    "PUT" => "reservas.local.editar",
                    "DELETE" => "reservas.local.excluir",
                    _ => null
                };
            }

            if (path.StartsWith("/api/reservas"))
            {
                return method switch
                {
                    "GET" => "reservas.reserva.visualizar",
                    "POST" => "reservas.reserva.criar",
                    "PUT" => "reservas.reserva.editar",
                    "DELETE" => "reservas.reserva.excluir",
                    _ => null
                };
            }

            if (path.StartsWith("/api/recebiveis"))
            {
                return method switch
                {
                    "GET" => "reservas.recebivel.visualizar",
                    "POST" => "reservas.recebivel.criar",
                    "PUT" => "reservas.recebivel.editar",
                    "DELETE" => "reservas.recebivel.excluir",
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
