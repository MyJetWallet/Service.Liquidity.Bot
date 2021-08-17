using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
                    var msgText = "[Notification] Trade in portfolio." +
                                  $" {msg.BaseVolume} {msg.BaseAsset} (${msg.BaseVolumeInUsd}) to {msg.QuoteVolume} {msg.QuoteAsset} (${msg.QuoteVolumeInUsd})." +
                                  $" Source: spot-trades." +
                                  $" RPL: ${msg.TotalReleasePnl}.";

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
