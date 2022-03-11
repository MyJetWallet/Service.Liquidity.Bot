using System;
using System.Threading.Tasks;
using Autofac;
using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using Service.Liquidity.Bot.Domain;
using Service.Liquidity.Monitoring.Domain.Models.RuleSets;

namespace Service.Liquidity.Bot.Subscribers
{
    public class MonitoringNotificationMessageSubscriber : IStartable
    {
        private ISubscriber<MonitoringNotificationMessage> _subscriber;
        private readonly ILogger<MonitoringNotificationMessageSubscriber> _logger;
        private readonly INotificationSender _notificationSender;

        public MonitoringNotificationMessageSubscriber(
            ILogger<MonitoringNotificationMessageSubscriber> logger,
            ISubscriber<MonitoringNotificationMessage> subscriber,
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

        private async ValueTask Handle(MonitoringNotificationMessage message)
        {
            try
            {
                await _notificationSender.SendAsync(message.ChannelId, message.Text);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e, "{sub} failed {@context}", nameof(MonitoringNotificationMessageSubscriber),
                    message);
            }
        }
    }
}