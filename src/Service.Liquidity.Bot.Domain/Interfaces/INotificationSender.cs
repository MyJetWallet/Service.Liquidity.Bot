namespace Service.Liquidity.Bot.Domain;

public interface INotificationSender
{
    Task SendAsync(string channelId, string text);
}