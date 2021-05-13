using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreDecorators;
using Grpc.Core.Logging;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Service.Liquidity.Engine.Domain.Models.Portfolio;

namespace Service.Liquidity.Bot.Services
{
    public class PositionPortfolioNotificatorJob
    {
        private readonly ILogger<PositionPortfolioNotificatorJob> _logger;

        public PositionPortfolioNotificatorJob(ILogger<PositionPortfolioNotificatorJob> logger, ISubscriber<IReadOnlyList<PositionPortfolio>> subscriber)
        {
            _logger = logger;
            subscriber.Subscribe(HandleMessage);
        }

        private async ValueTask HandleMessage(IReadOnlyList<PositionPortfolio> arg)
        {
            Console.WriteLine(JsonConvert.SerializeObject(arg));
        }
    }
}
