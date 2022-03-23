using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace Service.Liquidity.Bot.Domain.Services
{
    public abstract class BaseRetryService<T>
    {
        protected readonly AsyncRetryPolicy RetryPolicy;

        protected BaseRetryService(ILogger<T> logger)
        {
            RetryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(3,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    onRetry: (exception, retryCount, context) =>
                    {
                        logger.LogWarning(
                            $"Failed request {context.OperationKey}, retrying {retryCount}. {exception.Message} {exception.StackTrace}");
                    });
        }
    }
}