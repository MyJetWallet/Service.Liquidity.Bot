using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using Service.Liquidity.Bot.Domain.Extensions;
using Service.Liquidity.Bot.Domain.Interfaces;
using Service.Liquidity.Bot.Domain.Models;
using Service.Liquidity.Monitoring.Domain.Models;
using Service.Liquidity.Monitoring.Domain.Models.RuleSets;

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

        private async ValueTask Handle(PortfolioMonitoringMessage message)
        {
            try
            {
                foreach (var ruleSet in message.RuleSets ?? Array.Empty<MonitoringRuleSet>())
                {
                    foreach (var rule in ruleSet.Rules ?? Array.Empty<MonitoringRule>())
                    {
                        var lastNotificationDate = await _notificationsCache.GetLastNotificationDateAsync(rule.Id);

                        if (rule.NeedsNotification(lastNotificationDate))
                        {
                            await _notificationSender.SendAsync(rule.NotificationChannelId,
                                rule.GetNotificationText(message.Checks));
                            await _notificationsCache.AddOrUpdateAsync(rule.Id, DateTime.UtcNow.AddHours(1));
                        }
                    }
                }

                foreach (var rule in message.Rules?
                             .Where(r => r.Category == MonitoringRuleConsts.Category) ?? new List<MonitoringRule>())
                {
                    var lastNotificationDate = await _notificationsCache.GetLastNotificationDateAsync(rule.Id);

                    if (rule.NeedsNotification(lastNotificationDate))
                    {
                        var channelId = rule.ParamsByName?.Values
                            .FirstOrDefault(p => p.Name == MonitoringRuleConsts.ChannelIdParam)?
                            .GetString();
                        await _notificationSender.SendAsync(channelId!, rule.GetNotificationText());
                        await _notificationsCache.AddOrUpdateAsync(rule.Id, DateTime.UtcNow.AddHours(1));
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation(e, "{sub} failed {@context}", nameof(PortfolioMonitoringMessageSubscriber),
                    message);
            }
        }
    }
}