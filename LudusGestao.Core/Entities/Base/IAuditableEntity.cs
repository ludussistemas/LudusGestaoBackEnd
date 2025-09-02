namespace LudusGestao.Core.Entities.Base
{
    public interface IAuditableEntity
    {
        DateTime DataCriacao { get; set; }
        DateTime? DataAtualizacao { get; set; }
    }
}
