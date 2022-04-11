using Service.IntrestManager.Domain.Models;

namespace Service.Liquidity.Bot.Domain.Extensions;

public static class InterestProcessingResultExtensions
{
    public static string GetNotificationText(this InterestProcessingResult src)
    {
        return $"Interest rates are paid:{Environment.NewLine}" +
               $"Total paid amount: {src.TotalPaidAmountInUsd}${Environment.NewLine}" +
               $"Completed count: {src.CompletedCount}{Environment.NewLine}" +
               $"Failed count: {src.FailedCount}";
    }
}