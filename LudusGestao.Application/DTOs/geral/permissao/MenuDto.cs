namespace LudusGestao.Application.DTOs.geral.permissao
{
    public class MenuDto
    {
        public List<ModuloMenuDto> Modulos { get; set; } = new List<ModuloMenuDto>();
    }

    public class ModuloMenuDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public List<SubmoduloMenuDto> Submodulos { get; set; } = new List<SubmoduloMenuDto>();
    }

    public class SubmoduloMenuDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
    }
}
