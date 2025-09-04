using LudusGestao.Domain.DTOs.Empresa;
using LudusGestao.Domain.DTOs.Filial;
using LudusGestao.Domain.DTOs.Usuario;

namespace LudusGestao.Domain.DTOs.Gerencialmento
{
    public class NovoClienteResultadoDTO
    {
        public int TenantId { get; set; }
        public EmpresaDTO Empresa { get; set; }
        public FilialDTO FilialMatriz { get; set; }
        public UsuarioDTO UsuarioAdmin { get; set; }
        public string SenhaPadrao { get; set; }
    }
}
