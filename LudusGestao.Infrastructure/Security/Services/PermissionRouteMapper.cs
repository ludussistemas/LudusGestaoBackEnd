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
                    "GET" => "Configuracoes.Empresa.Visualizar",
                    "POST" => "Configuracoes.Empresa.Criar",
                    "PUT" => "Configuracoes.Empresa.Editar",
                    "DELETE" => "Configuracoes.Empresa.Excluir",
                    _ => null
                };
            }

            if (path.StartsWith("/api/filiais"))
            {
                return method switch
                {
                    "GET" => "Configuracoes.Filiais.Visualizar",
                    "POST" => "Configuracoes.Filiais.Criar",
                    "PUT" => "Configuracoes.Filiais.Editar",
                    "DELETE" => "Configuracoes.Filiais.Excluir",
                    _ => null
                };
            }

            if (path.StartsWith("/api/usuarios"))
            {
                return method switch
                {
                    "GET" => "Configuracoes.Usuarios.Visualizar",
                    "POST" => "Configuracoes.Usuarios.Criar",
                    "PUT" => "Configuracoes.Usuarios.Editar",
                    "DELETE" => "Configuracoes.Usuarios.Excluir",
                    _ => null
                };
            }

            if (path.StartsWith("/api/grupos-permissoes"))
            {
                return method switch
                {
                    "GET" => "Configuracoes.Grupo de Permissões.Visualizar",
                    "POST" => "Configuracoes.Grupo de Permissões.Criar",
                    "PUT" => "Configuracoes.Grupo de Permissões.Editar",
                    "DELETE" => "Configuracoes.Grupo de Permissões.Excluir",
                    _ => null
                };
            }

            if (path.StartsWith("/api/usuarios-permissoes"))
            {
                return method switch
                {
                    "GET" => "Configuracoes.Usuarios.Visualizar",
                    "POST" => "Configuracoes.Usuarios.Criar",
                    "PUT" => "Configuracoes.Usuarios.Editar",
                    "DELETE" => "Configuracoes.Usuarios.Excluir",
                    _ => null
                };
            }

            if (path.StartsWith("/api/clientes"))
            {
                return method switch
                {
                    "GET" => "Eventos.Clientes.Visualizar",
                    "POST" => "Eventos.Clientes.Criar",
                    "PUT" => "Eventos.Clientes.Editar",
                    "DELETE" => "Eventos.Clientes.Excluir",
                    _ => null
                };
            }

            if (path.StartsWith("/api/locais"))
            {
                return method switch
                {
                    "GET" => "Eventos.Locais.Visualizar",
                    "POST" => "Eventos.Locais.Criar",
                    "PUT" => "Eventos.Locais.Editar",
                    "DELETE" => "Eventos.Locais.Excluir",
                    _ => null
                };
            }

            if (path.StartsWith("/api/reservas"))
            {
                return method switch
                {
                    "GET" => "Eventos.Reservas.Visualizar",
                    "POST" => "Eventos.Reservas.Criar",
                    "PUT" => "Eventos.Reservas.Editar",
                    "DELETE" => "Eventos.Reservas.Excluir",
                    _ => null
                };
            }

            if (path.StartsWith("/api/recebiveis"))
            {
                return method switch
                {
                    "GET" => "Eventos.Recebiveis.Visualizar",
                    "POST" => "Eventos.Recebiveis.Criar",
                    "PUT" => "Eventos.Recebiveis.Editar",
                    "DELETE" => "Eventos.Recebiveis.Excluir",
                    _ => null
                };
            }

            return null;
        }

        public string GetParentModule(string path)
        {
            if (path.StartsWith("/api/empresas") || path.StartsWith("/api/filiais") ||
                path.StartsWith("/api/usuarios") || path.StartsWith("/api/grupos-permissoes") ||
                path.StartsWith("/api/usuarios-permissoes"))
            {
                return "Configuracoes";
            }

            if (path.StartsWith("/api/clientes") || path.StartsWith("/api/locais") ||
                path.StartsWith("/api/reservas") || path.StartsWith("/api/recebiveis"))
            {
                return "Eventos";
            }

            return null;
        }
    }
}
