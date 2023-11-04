using CryptoBot.Domain.Models.Entities;

namespace CryptoBot.Domain;

public interface IKlineService : IService<KlineEntity, long>
{
    Task StartTrackingBtc();
    Task StartTrackingUsdt();
    void StopTracking();
}
