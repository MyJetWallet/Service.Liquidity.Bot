using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using Service.Liquidity.Engine.Domain.Models.Portfolio;
using Telegram.Bot;

namespace Service.Liquidity.Bot.Services
{
    public class PositionPortfolioNotificatorJob
    {
        private readonly ILogger<PositionPortfolioNotificatorJob> _logger;
        private readonly ITelegramBotClient _botApiClient;

        public PositionPortfolioNotificatorJob(ILogger<PositionPortfolioNotificatorJob> logger, ISubscriber<IReadOnlyList<PositionPortfolio>> subscriber)
        {
            _logger = logger;
            _botApiClient = new TelegramBotClient(Program.Settings.BotApiKey);
            subscriber.Subscribe(PositionPortfolioUpdateHandler);
        }

        private async ValueTask PositionPortfolioUpdateHandler(IReadOnlyList<PositionPortfolio> messages)
        {
            var sentCounter = 0;

            foreach (var msg in messages.Where(m => !m.IsOpen))
            {
                try
                {
                    var smile = msg.ResultPercentage >= 0 ? "😃" : "😨";
                    var msgText = $"{smile} {msg.Symbol} {msg.Side.ToString().ToLower()} {Math.Abs(msg.TotalBaseVolume)}; Result: {msg.QuoteVolume} {msg.QuotesAsset} ({msg.ResultPercentage}%)";
                    await _botApiClient.SendTextMessageAsync(Program.Settings.ChatId, msgText);

                    sentCounter++;
                }
                catch (Exception e)
                {
                    _logger.Log(LogLevel.Debug, e.Message);
                }
            }

            _logger.Log(LogLevel.Information, $"Messages sent: {sentCounter}");
        }
    }
}
