using LudusGestao.Domain.DTOs.Gerencialmento;
using LudusGestao.Core.Models;

namespace LudusGestao.Domain.Interfaces.Services.geral
{
    public interface IGerencialmentoService
    {
        Task<ApiResponse<NovoClienteResultadoDTO>> CriarNovoCliente(CriarNovoClienteDTO dto);
        Task<ApiResponse<object>> AlterarSenha(AlterarSenhaDTO dto);
    }
}
