namespace LudusGestao.Domain.Interfaces.Services.infra
{
    public interface IFilialService
    {
        void SetFilialId(Guid filialId);
        Guid GetFilialId();
        bool TryGetFilialId(out Guid filialId);
    }
}


