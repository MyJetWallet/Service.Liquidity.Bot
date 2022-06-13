using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using DotNetCoreDecorators;
using Humanizer;
using Microsoft.Extensions.Logging;
using Service.IntrestManager.Domain.Models;
using Service.Liquidity.Alerts.Domain.Models.Alerts;
using Service.Liquidity.Alerts.Domain.Models.Channels;
using Service.Liquidity.Bot.Domain.Extensions;
using Service.Liquidity.Bot.Domain.Interfaces;
using Service.Liquidity.Bot.Domain.Models;
using Service.Liquidity.Bot.Settings;
using Service.Liquidity.Monitoring.Domain.Models;
using Service.Liquidity.Monitoring.Domain.Models.Rules;

namespace Service.Liquidity.Bot.Subscribers
{
    public class NewAlertMessageSubscriber : IStartable
    {
        private readonly ISubscriber<NewAlertMessage> _subscriber;
        private readonly ILogger<NewAlertMessageSubscriber> _logger;
        private readonly INotificationSender _notificationSender;

        public NewAlertMessageSubscriber(
            ILogger<NewAlertMessageSubscriber> logger,
            ISubscriber<NewAlertMessage> subscriber,
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

        private ValueTask Handle(NewAlertMessage message)
        {
            _ = Task.Run(async () =>
            {
                try
                {
                    var telegramChannels = message.AlertChannels?
                        .Where(c => c.Type == AlertChannelType.Telegram)
                        .ToArray() ?? Array.Empty<AlertChannel>();

                    if (telegramChannels.Any())
                    {
                        var text = $"{message.Alert.EventType.Humanize()}{Environment.NewLine}{message.Alert.Message}";

                        foreach (var channel in telegramChannels)
                        {
                            if (channel.Params.TryGetValue(AlertChannelParam.TelegramChatId, out var chatId))
                            {
                                await _notificationSender.SendToChat(chatId, text);
                            }
                        }
                    }
                    else if
                        (message.Alert.Destinations.HasFlag(AlertDestinations
                            .Telegram)) // todo: remove after migration to channels
                    {
                        var text = $"{message.Alert.EventType.Humanize()}{Environment.NewLine}{message.Alert.Message}";
                        await _notificationSender.SendAsync(text);
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Failed to handle {@Message}. {@ExMessage}", nameof(NewAlertMessage),
                        e.Message);
                }
            });

            return ValueTask.CompletedTask;
        }
    }
}