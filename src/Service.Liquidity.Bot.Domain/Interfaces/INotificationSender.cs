namespace Service.Liquidity.Bot.Domain.Interfaces;

public interface INotificationSender
{
    Task SendAsync(string channelId, string text);
    Task SendAsync(string text);
}