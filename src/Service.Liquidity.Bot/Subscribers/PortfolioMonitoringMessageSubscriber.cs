using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using Service.Liquidity.Bot.Domain.Extensions;
using Service.Liquidity.Bot.Domain.Interfaces;
using Service.Liquidity.Bot.Domain.Models;
using Service.Liquidity.Bot.Settings;
using Service.Liquidity.Monitoring.Domain.Models;
using Service.Liquidity.Monitoring.Domain.Models.Rules;

namespace Service.Liquidity.Bot.Subscribers
{
    public class PortfolioMonitoringMessageSubscriber : IStartable
    {
        private readonly ISubscriber<PortfolioMonitoringMessage> _subscriber;
        private readonly ILogger<PortfolioMonitoringMessageSubscriber> _logger;
        private readonly INotificationSender _notificationSender;
        private readonly INotificationsCache _notificationsCache;

        public PortfolioMonitoringMessageSubscriber(
            ILogger<PortfolioMonitoringMessageSubscriber> logger,
            ISubscriber<PortfolioMonitoringMessage> subscriber,
            INotificationSender notificationSender,
            INotificationsCache notificationsCache
        )
        {
            _subscriber = subscriber;
            _logger = logger;
            _notificationSender = notificationSender;
            _notificationsCache = notificationsCache;
        }

        public void Start()
        {
            _subscriber.Subscribe(Handle);
        }

        private ValueTask Handle(PortfolioMonitoringMessage message)
        {
            _ = Task.Run(async () =>
            {
                try
                {
                    foreach (var rule in message.Rules ?? new List<MonitoringRule>())
                    {
                        await Task.Delay(850);
                        await ProcessRuleAsync(rule);
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Failed to handle {@Message}. {@ExMessage}", nameof(PortfolioMonitoringMessage),
                        e.Message);
                }
            });

            return ValueTask.CompletedTask;
        }

        private async Task ProcessRuleAsync(MonitoringRule rule)
        {
            try
            {
                var lastNotificationDate = await _notificationsCache.GetLastNotificationDateAsync(rule.Id);

                if (rule.NeedsNotification(lastNotificationDate))
                {
                    var action = new SendNotificationMonitoringAction();
                    rule.ActionsByTypeName[action.TypeName].CopyTo(action);

                    if (string.IsNullOrWhiteSpace(action.NotificationChannelId))
                    {
                        await _notificationSender.SendAsync(rule.GetNotificationText());
                    }
                    else
                    {
                        await _notificationSender.SendAsync(action.NotificationChannelId, rule.GetNotificationText());
                    }

                    await _notificationsCache.AddOrUpdateAsync(rule.Id,
                        DateTime.UtcNow.Add(MonitoringRuleExtensions.NotificationRemindPeriod));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to ProcessRule. {@Rule}", rule.Name);
            }
        }
    }
}