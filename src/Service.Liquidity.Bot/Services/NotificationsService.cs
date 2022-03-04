using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Service.Liquidity.Bot.Domain;
using Service.Liquidity.Bot.Grpc;
using Service.Liquidity.Bot.Grpc.Models.Notifications;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Service.Liquidity.Bot.Services
{
    public class NotificationsService : INotificationsService
    {
        private readonly ILogger<NotificationsService> _logger;
        private readonly INotificationChannelsRepository _notificationChannelsRepository;
        private readonly ITelegramBotClient _telegramBotClient;

        public NotificationsService(
            ILogger<NotificationsService> logger,
            INotificationChannelsRepository notificationChannelsRepository,
            ITelegramBotClient telegramBotClient
            )
        {
            _logger = logger;
            _notificationChannelsRepository = notificationChannelsRepository;
            _telegramBotClient = telegramBotClient;
        }

        public async Task<SendNotificationResponse> SendAsync(SendNotificationRequest request)
        {
            try
            {
                var channel = await _notificationChannelsRepository.GetAsync(request.ChannelId);

                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                if (channel == null)
                {
                    _logger.LogWarning("Can't send notification, channel with id {channelId} not found",
                        request.ChannelId);
                    return new SendNotificationResponse()
                    {
                        IsError = true,
                        ErrorMessage = $"Channel with id {request.ChannelId} not found"
                    };
                }

                await _telegramBotClient.SendTextMessageAsync(channel.ChatId,
                    request.Text, ParseMode.Html);
                
                return new SendNotificationResponse();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Failed to SendMessage {@request}", request);
                return new SendNotificationResponse
                {
                    IsError = true,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}