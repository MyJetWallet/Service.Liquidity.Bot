using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Service.Liquidity.Bot.Domain.Interfaces;
using Service.Liquidity.Bot.Domain.Models;
using Service.Liquidity.Bot.Settings;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Service.Liquidity.Bot.Services
{
    public class NotificationTelegramSender : INotificationSender
    {
        private readonly ILogger<NotificationTelegramSender> _logger;
        private readonly INotificationChannelsRepository _notificationChannelsRepository;
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly SettingsModel _settingsModel;

        public NotificationTelegramSender(
            ILogger<NotificationTelegramSender> logger,
            INotificationChannelsRepository notificationChannelsRepository,
            ITelegramBotClient telegramBotClient,
            SettingsModel settingsModel
        )
        {
            _logger = logger;
            _notificationChannelsRepository = notificationChannelsRepository;
            _telegramBotClient = telegramBotClient;
            _settingsModel = settingsModel;
        }

        public async Task SendAsync(string text)
        {
            try
            {
                await _telegramBotClient.SendTextMessageAsync(_settingsModel.ChatId, text, ParseMode.Html);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to SendNotification: {text}");
            }
        }

        public async Task SendAsync(string channelId, string text)
        {
            NotificationChannel channel = null;

            try
            {
                channel = await _notificationChannelsRepository.GetAsync(channelId);

                if (channel == null)
                {
                    throw new Exception($"Failed to SendNotification. Channel with id {channelId} not found");
                }

                await _telegramBotClient.SendTextMessageAsync(channel.ChatId, text, ParseMode.Html);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to SendNotification to {channel?.Name} {text}");
            }
        }
    }
}