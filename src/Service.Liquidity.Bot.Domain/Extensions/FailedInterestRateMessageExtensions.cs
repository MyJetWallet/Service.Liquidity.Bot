using Service.IntrestManager.Domain.Models;

namespace Service.Liquidity.Bot.Domain.Extensions;

public static class FailedInterestRateMessageExtensions
{
    public static string GetNotificationText(this FailedInterestRateMessage src)
    {
        return $"Failed to pay interest rate:{Environment.NewLine}" +
               $"{src.WalletId}{Environment.NewLine}" +
               $"{src.ErrorMessage}{Environment.NewLine}" +
               $"Date: {src.Date:yyyy-MM-dd hh:mm:ss}";
    }
}