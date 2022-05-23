using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Service.Liquidity.Bot.Domain.Interfaces;
using Service.Liquidity.Bot.Domain.Models;
using Service.Liquidity.Bot.Domain.Services;
using Service.Liquidity.Bot.Settings;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Service.Liquidity.Bot.Services
{
    public class NotificationTelegramSender : BaseRetryService<NotificationTelegramSender>, INotificationSender
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
            : base(logger)
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
                await RetryPolicy.ExecuteAsync(async () =>
                    await _telegramBotClient.SendTextMessageAsync(_settingsModel.ChatId, text, ParseMode.Html));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send message to telegram to chat. {@ChatId}. {@ExMessage}", _settingsModel.ChatId,
                    ex.Message);
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
                    throw new Exception($"Failed to to send message to telegram. Channel with id {channelId} not found");
                }

                await RetryPolicy.ExecuteAsync(async () =>
                    await _telegramBotClient.SendTextMessageAsync(channel.ChatId, text, ParseMode.Html));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send message to telegram by channel {@Channel}. {@ExMessage}",
                    channel?.Name, ex.Message);
            }
        }
    }
}