using System;
using System.Threading.Tasks;
using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using Service.Liquidity.Monitoring.Domain.Models;
using Telegram.Bot;

namespace Service.Liquidity.Bot.Subscribers
{
    public class LiquidityMessageSubscriber
    {
        private readonly ILogger<LiquidityMessageSubscriber> _logger;
        private readonly ITelegramBotClient _botApiClient;

        public LiquidityMessageSubscriber(
            ILogger<LiquidityMessageSubscriber> logger,
            ISubscriber<AssetPortfolioStatusMessage> subscriber, 
            ITelegramBotClient botApiClient)
        {
            _logger = logger;
            _botApiClient = botApiClient;
            subscriber.Subscribe(Consume);
        }

        private async ValueTask Consume(AssetPortfolioStatusMessage message)
        {
            _logger.LogInformation("Consuming message {@context}", message);
            
            try
            {
                await _botApiClient.SendTextMessageAsync(Program.Settings.ChatId, message.Message);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e, "Error during consumptions {@context}", message);
                throw;
            }
            _logger.LogInformation("Has been consumed {@context}", message);            
        }
    }
}