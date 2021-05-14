using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Service.Liquidity.Bot.Constants;
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
            subscriber.Subscribe(PositionPortfolioUpdateHandler);

            _botApiClient = new TelegramBotClient(Program.Settings.BotApiKey);
        }

        public async Task<bool> SenderBotStart()
        {
            if (await _botApiClient.TestApiAsync())
            {
                _botApiClient.StartReceiving();

                return Result.SUCCESS;
            }

            return Result.FAILURE;
        }

        private async ValueTask PositionPortfolioUpdateHandler(IReadOnlyList<PositionPortfolio> arg)
        {
            foreach (var positionPortfolio in arg)
            {
                //var msg = $"! {positionPortfolio.Symbol}"; // TODO
                await _botApiClient.SendTextMessageAsync(Program.Settings.ChatId, JsonConvert.SerializeObject(arg));
            }
        }
    }
}
