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
    public class PortfolioSettlementNotificator : IStartable
    {
        private readonly ILogger<PortfolioSettlementNotificator> _logger;
        private readonly ITelegramBotClient _botApiClient;

        public PortfolioSettlementNotificator(ILogger<PortfolioSettlementNotificator> logger,
            ISubscriber<IReadOnlyList<ManualSettlement>> subscriber,
            ITelegramBotClient botApiClient)
        {
            _logger = logger;
            _botApiClient = botApiClient;
            subscriber.Subscribe(Handle);
        }

        private async ValueTask Handle(IReadOnlyList<ManualSettlement> messages)
        {
            var sentCounter = 0;

            foreach (var msg in messages)
            {
                try
                {
                    var msgText = $"[Notification] Settlement in portfolio." +
                                  $" {msg.VolumeFrom} {msg.Asset} from {msg.WalletFrom} to {msg.VolumeTo} {msg.Asset} {msg.WalletTo}." +
                                  $" {msg.User} said: {msg.Comment}.";
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