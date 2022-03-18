namespace Service.Liquidity.Bot.Domain.Interfaces;

public interface INotificationsCache
{
    Task AddOrUpdateAsync(string ruleId, DateTime expires);
    Task<DateTime> GetLastNotificationDateAsync(string ruleId);
}