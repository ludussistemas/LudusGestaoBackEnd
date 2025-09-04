namespace LudusGestao.Domain.DTOs.infra.Utilitarios;

public class EnderecoCepDTO
{
    public string Cep { get; set; }
    public string Rua { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public string Numero { get; set; } = "";
}
