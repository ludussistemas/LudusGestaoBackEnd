namespace LudusGestao.Domain.DTOs.infra.Utilitarios;

public class LocalResumoDTO
{
    public int TotalLocais { get; set; }
    public int Ativos { get; set; }
    public int Inativos { get; set; }
    public int Manutencao { get; set; }
    public decimal ValorMedioHora { get; set; }
}
