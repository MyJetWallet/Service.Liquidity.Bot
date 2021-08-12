using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Service.Liquidity.Portfolio.Domain.Models;
using Telegram.Bot;

namespace Service.Liquidity.Bot.Services
{
    public class PortfolioTradeNotificatorJob
    {
        private readonly ILogger<PortfolioTradeNotificatorJob> _logger;
        private readonly ITelegramBotClient _botApiClient;

        public PortfolioTradeNotificatorJob(ILogger<PortfolioTradeNotificatorJob> logger, ISubscriber<IReadOnlyList<AssetPortfolioTrade>> subscriber)
        {
            _logger = logger;
            _botApiClient = new TelegramBotClient(Program.Settings.BotApiKey);
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
