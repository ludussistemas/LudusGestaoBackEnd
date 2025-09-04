// Arquivo criado para padronização da estrutura de DTOs 

namespace LudusGestao.Core.DTOs;

/// <summary>
/// DTO base para herança de outros DTOs.
/// </summary>
public abstract class BaseDTO
{
    public Guid Id { get; set; }
}
