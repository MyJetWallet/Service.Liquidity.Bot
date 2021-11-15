using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using Service.Liquidity.Portfolio.Domain.Models;
using Telegram.Bot;

namespace Service.Liquidity.Bot.Job
{
    public class PortfolioTradeNotificator : IStartable
    {
        private readonly ILogger<PortfolioTradeNotificator> _logger;
        private readonly ITelegramBotClient _botApiClient;

        public PortfolioTradeNotificator(ILogger<PortfolioTradeNotificator> logger,
            ISubscriber<IReadOnlyList<AssetPortfolioTrade>> subscriber,
            ITelegramBotClient botApiClient)
        {
            _logger = logger;
            _botApiClient = botApiClient;
            subscriber.Subscribe(Handle);
        }

        private async ValueTask Handle(IReadOnlyList<AssetPortfolioTrade> messages)
        {
            var sentCounter = 0;

            foreach (var msg in messages)
            {
                try
                {
                    var baseVolumeRound = Math.Round(msg.BaseVolume, 8);
                    var baseVolumeUsdRound = Math.Round(msg.BaseVolumeInUsd, 2);
                    var quoteVolumeRound = Math.Round(msg.QuoteVolume, 8);
                    var quoteVolumeInUsdRound = Math.Round(msg.QuoteVolumeInUsd, 2);
                    var totalReleasePnlRound = Math.Round(msg.TotalReleasePnl, 2);
                    
                    var msgText = "[Notification] Trade in portfolio." +
                                  $" {baseVolumeRound} {msg.BaseAsset} (${baseVolumeUsdRound}) to {quoteVolumeRound} {msg.QuoteAsset} (${quoteVolumeInUsdRound})." +
                                  $" Source: {msg.Source}." +
                                  $" RPL: ${totalReleasePnlRound}.";

                    if (!string.IsNullOrWhiteSpace(msg.Comment))
                    {
                        msgText += $" Comment: {msg.Comment}" + 
                                  $" User: {msg.User}";
                    }

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

        public void Start()
        {
        }
    }
}
