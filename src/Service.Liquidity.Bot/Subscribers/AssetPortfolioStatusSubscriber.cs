using System;
using System.Threading.Tasks;
using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.Service;
using Service.Liquidity.Monitoring.Domain.Models;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Service.Liquidity.Bot.Subscribers
{
    public class AssetPortfolioStatusSubscriber
    {
        private readonly ILogger<AssetPortfolioStatusSubscriber> _logger;
        private readonly ITelegramBotClient _botApiClient;

        public AssetPortfolioStatusSubscriber(
            ILogger<AssetPortfolioStatusSubscriber> logger,
            ISubscriber<AssetPortfolioStatusMessage> subscriber, 
            ITelegramBotClient botApiClient)
        {
            _logger = logger;
            _botApiClient = botApiClient;
            subscriber.Subscribe(Consume);
        }

        private async ValueTask Consume(AssetPortfolioStatusMessage message)
        {
            try
            {
                var uniMessage = message.Message;
                _logger.LogInformation("Ready to send message {message}", uniMessage);
                var result = await _botApiClient.SendTextMessageAsync(Program.Settings.ChatId, 
                    uniMessage, ParseMode.Html);
                _logger.LogInformation("Message was sent with id {id}", result.MessageId);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e, "Error during consumptions {@context}", message);
            }
        }
    }
}