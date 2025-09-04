// Arquivo criado para padronização da estrutura de DTOs 

namespace LudusGestao.Domain.DTOs.Filial;
using LudusGestao.Domain.Enums.geral;
using System;

public class CreateFilialDTO
{
    public string Nome { get; set; }
    public string Codigo { get; set; }
    public string Endereco { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public string Cep { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public string Cnpj { get; set; }
    public string Responsavel { get; set; }
    public SituacaoBase Situacao { get; set; }
    public DateTime DataAbertura { get; set; }
    public Guid EmpresaId { get; set; }
}
