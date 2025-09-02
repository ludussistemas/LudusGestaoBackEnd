namespace LudusGestao.Domain.Interfaces.Services.infra
{
    public interface ISeedService
    {
        Task<bool> SeedDadosBaseAsync();
    }
}
