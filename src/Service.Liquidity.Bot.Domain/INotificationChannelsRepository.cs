using Service.Liquidity.Bot.Domain.Models;

namespace Service.Liquidity.Bot.Domain;

public interface INotificationChannelsRepository
{
    Task<IEnumerable<NotificationChannel>> GetAsync();
    Task AddOrUpdateAsync(NotificationChannel model);
    Task<NotificationChannel> GetAsync(string id);
    Task DeleteAsync(string id);
}