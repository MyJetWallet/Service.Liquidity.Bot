using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using DotNetCoreDecorators;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Service.Liquidity.Bot.Domain;
using Service.Liquidity.Bot.Domain.Extensions;
using Service.Liquidity.Monitoring.Domain.Models;
using Service.Liquidity.Monitoring.Domain.Models.Checks;
using Service.Liquidity.Monitoring.Domain.Models.RuleSets;

namespace Service.Liquidity.Bot.Subscribers
{
    public class PortfolioMonitoringMessageSubscriber : IStartable
    {
        private readonly ISubscriber<PortfolioMonitoringMessage> _subscriber;
        private readonly ILogger<PortfolioMonitoringMessageSubscriber> _logger;
        private readonly INotificationSender _notificationSender;
        private readonly IMemoryCache _memoryCache;

        public PortfolioMonitoringMessageSubscriber(
            ILogger<PortfolioMonitoringMessageSubscriber> logger,
            ISubscriber<PortfolioMonitoringMessage> subscriber,
            INotificationSender notificationSender,
            IMemoryCache memoryCache
        )
        {
            _subscriber = subscriber;
            _logger = logger;
            _notificationSender = notificationSender;
            _memoryCache = memoryCache;
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
                        _memoryCache.TryGetValue(GetCacheKey(rule), out DateTime? lastNotificationDate);
                        
                        if (rule.NeedsNotification(lastNotificationDate))
                        {
                            await _notificationSender.SendAsync(rule.NotificationChannelId, rule.GetNotificationText(message.Checks));
                            _memoryCache.Set(GetCacheKey(rule), DateTime.UtcNow, TimeSpan.FromMinutes(60));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation(e, "{sub} failed {@context}", nameof(PortfolioMonitoringMessageSubscriber),
                    message);
            }
        }

        private string GetCacheKey(MonitoringRule rule)
        {
            return rule.Id;
        }
    }
}