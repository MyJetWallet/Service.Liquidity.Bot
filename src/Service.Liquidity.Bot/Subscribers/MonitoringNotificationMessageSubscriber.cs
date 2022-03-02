using System;
using System.Threading.Tasks;
using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using Service.Liquidity.Bot.Domain;
using Service.Liquidity.Bot.Domain.Models;
using Service.Liquidity.Monitoring.Domain.Models;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Service.Liquidity.Bot.Subscribers
{
    public class MonitoringNotificationMessageSubscriber
    {
        private readonly ILogger<MonitoringNotificationMessageSubscriber> _logger;
        private readonly ITelegramBotClient _botApiClient;
        private readonly INotificationChannelsRepository _channelsRepository;

        public MonitoringNotificationMessageSubscriber(
            ILogger<MonitoringNotificationMessageSubscriber> logger,
            ISubscriber<MonitoringNotificationMessage> subscriber,
            ITelegramBotClient botApiClient,
            INotificationChannelsRepository channelsRepository
        )
        {
            _logger = logger;
            subscriber.Subscribe(Consume);
            _botApiClient = botApiClient;
            _channelsRepository = channelsRepository;
        }

        private async ValueTask Consume(MonitoringNotificationMessage message)
        {
            try
            {
                var channel = await _channelsRepository.GetAsync(message.ChannelId);

                if (channel == null)
                {
                    _logger.LogWarning("Can't send notification, channel with id {channelId} not found",
                        message.ChannelId);
                    return;
                }

                await _botApiClient.SendTextMessageAsync(channel.ChatId,
                    message.Text, ParseMode.Html);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e, "{sub} failed {@context}", nameof(MonitoringNotificationMessageSubscriber),
                    message);
            }
        }
    }
}