using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Service.Liquidity.Portfolio.Domain.Models;
using Telegram.Bot;

namespace Service.Liquidity.Bot.Job
{
    public class PortfolioTradeNotificator
    {
        private readonly ILogger<PortfolioTradeNotificator> _logger;
        private readonly ITelegramBotClient _botApiClient;

        public PortfolioTradeNotificator(ILogger<PortfolioTradeNotificator> logger,
            ISubscriber<IReadOnlyList<AssetPortfolioTrade>> subscriber,
            ITelegramBotClient botApiClient)
        {
            _logger = logger;
            _botApiClient = botApiClient;
            subscriber.Subscribe(PortfolioTradeHandler);
        }

        private async ValueTask PortfolioTradeHandler(IReadOnlyList<AssetPortfolioTrade> messages)
        {
            var sentCounter = 0;

            foreach (var msg in messages)
            {
                try
                {
                    var msgText = $"Received new trade from portfolio: {JsonConvert.SerializeObject(msg, Formatting.Indented)}%)";
                    await _botApiClient.SendTextMessageAsync(Program.Settings.ChatId, msgText);

                    sentCounter++;
                }
                catch (Exception e)
                {
                    _logger.LogDebug(e.Message);
                }
            }
            _logger.LogInformation($"Messages sent: {sentCounter}");
        }
    }
}
