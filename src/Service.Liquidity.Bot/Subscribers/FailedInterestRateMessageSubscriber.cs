using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using Service.IntrestManager.Domain.Models;
using Service.Liquidity.Bot.Domain.Extensions;
using Service.Liquidity.Bot.Domain.Interfaces;
using Service.Liquidity.Bot.Domain.Models;
using Service.Liquidity.Bot.Settings;
using Service.Liquidity.Monitoring.Domain.Models;
using Service.Liquidity.Monitoring.Domain.Models.Rules;

namespace Service.Liquidity.Bot.Subscribers
{
    public class FailedInterestRateMessageSubscriber : IStartable
    {
        private readonly ISubscriber<IReadOnlyList<FailedInterestRateMessage>> _subscriber;
        private readonly ILogger<FailedInterestRateMessageSubscriber> _logger;
        private readonly INotificationSender _notificationSender;

        public FailedInterestRateMessageSubscriber(
            ILogger<FailedInterestRateMessageSubscriber> logger,
            ISubscriber<IReadOnlyList<FailedInterestRateMessage>> subscriber,
            INotificationSender notificationSender
            )
        {
            _subscriber = subscriber;
            _logger = logger;
            _notificationSender = notificationSender;
        }

        public void Start()
        {
            _subscriber.Subscribe(Handle);
        }

        private async ValueTask Handle(IReadOnlyList<FailedInterestRateMessage> messages)
        {
            try
            {
                if (messages == null || !messages.Any())
                {
                    return;
                }

                foreach (var item in messages)
                {
                    await _notificationSender.SendAsync(item.GetNotificationText());
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to handle {@Message}. {@ExMessage}", nameof(FailedInterestRateMessage),
                    e.Message);
            }
        }
    }
}