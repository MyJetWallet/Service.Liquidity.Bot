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
    public class InterestProcessingResultSubscriber : IStartable
    {
        private readonly ISubscriber<InterestProcessingResult> _subscriber;
        private readonly ILogger<InterestProcessingResultSubscriber> _logger;
        private readonly INotificationSender _notificationSender;

        public InterestProcessingResultSubscriber(
            ILogger<InterestProcessingResultSubscriber> logger,
            ISubscriber<InterestProcessingResult> subscriber,
            INotificationSender notificationSender
            )
        {
            _subscriber = subscriber;
            _logger = logger;
            _notificationSender = notificationSender;
        }

        public void Start()
        {
            //_subscriber.Subscribe(Handle);
        }

        private async ValueTask Handle(InterestProcessingResult message)
        {
            try
            {
                if (message == null)
                {
                    return;
                }

                await _notificationSender.SendAsync(message.GetNotificationText());
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to handle {@Message}. {@ExMessage}", nameof(FailedInterestRateMessage),
                    e.Message);
            }
        }
    }
}