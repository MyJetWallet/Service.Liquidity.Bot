using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Service.Liquidity.Bot.Domain.Interfaces;
using Service.Liquidity.Bot.Grpc;
using Service.Liquidity.Bot.Grpc.Models.Notifications;

namespace Service.Liquidity.Bot.Services
{
    public class NotificationsService : INotificationsService
    {
        private readonly ILogger<NotificationsService> _logger;
        private readonly INotificationSender _notificationSender;

        public NotificationsService(
            ILogger<NotificationsService> logger,
            INotificationSender notificationSender
        )
        {
            _logger = logger;
            _notificationSender = notificationSender;
        }

        public async Task<SendNotificationResponse> SendAsync(SendNotificationRequest request)
        {
            try
            {
                await _notificationSender.SendAsync(request.ChannelId, request.Text);

                return new SendNotificationResponse();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to SendMessage {@request}", request);
                return new SendNotificationResponse
                {
                    IsError = true,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}