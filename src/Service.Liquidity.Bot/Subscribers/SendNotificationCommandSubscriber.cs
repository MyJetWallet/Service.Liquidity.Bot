using System;
using System.Threading.Tasks;
using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using Service.Liquidity.Bot.Domain;
using Service.Liquidity.Bot.Domain.Models;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Service.Liquidity.Bot.Subscribers
{
    public class SendNotificationCommandSubscriber
    {
        private readonly ILogger<SendNotificationCommandSubscriber> _logger;
        private readonly ITelegramBotClient _botApiClient;
        private readonly INotificationChannelsRepository _channelsRepository;

        public SendNotificationCommandSubscriber(
            ILogger<SendNotificationCommandSubscriber> logger,
            ISubscriber<SendNotificationCommand> subscriber,
            ITelegramBotClient botApiClient,
            INotificationChannelsRepository channelsRepository
        )
        {
            _logger = logger;
            subscriber.Subscribe(Consume);
            _botApiClient = botApiClient;
            _channelsRepository = channelsRepository;
        }

        private async ValueTask Consume(SendNotificationCommand command)
        {
            try
            {
                var channel = await _channelsRepository.GetAsync(command.ChannelId);

                if (channel == null)
                {
                    _logger.LogWarning("Can't send notification, channel with id {channelId} not found", command.ChannelId);
                    return;
                }

                await _botApiClient.SendTextMessageAsync(command.ChannelId,
                    command.Message, ParseMode.Html);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e, "{sub} failed {@context}", nameof(SendNotificationCommandSubscriber),
                    command);
            }
        }
    }
}