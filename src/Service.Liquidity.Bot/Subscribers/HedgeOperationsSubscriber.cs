using System;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using Service.Liquidity.Bot.Domain.Interfaces;
using Service.Liquidity.Hedger.Domain.Models;

namespace Service.Liquidity.Bot.Subscribers
{
    public class HedgeOperationsSubscriber : IStartable
    {
        private readonly ILogger<HedgeOperationsSubscriber> _logger;
        private readonly ISubscriber<HedgeOperation> _subscriber;
        private readonly INotificationSender _notificationSender;

        public HedgeOperationsSubscriber(
            ILogger<HedgeOperationsSubscriber> logger,
            ISubscriber<HedgeOperation> subscriber,
            INotificationSender notificationSender
        )
        {
            _logger = logger;
            _subscriber = subscriber;
            _notificationSender = notificationSender;
        }

        public void Start()
        {
            _subscriber.Subscribe(Handle);
        }

        private async ValueTask Handle(HedgeOperation operation)
        {
            try
            {
                await _notificationSender.SendAsync(
                    $"Made hedge operation:" +
                    $"{Environment.NewLine}Asset={operation.HedgeTrades?.FirstOrDefault()?.BaseAsset} " +
                    $"Traded volume={operation.HedgeTrades?.Sum(t => t.BaseVolume) ?? 0}; " +
                    $"Target value={operation.TargetVolume}" +
                    $"{Environment.NewLine}Date: {operation.CreatedDate:yyyy-MM-dd hh:mm:ss}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to handle {nameof(HedgeOperation)}");
            }
        }
    }
}