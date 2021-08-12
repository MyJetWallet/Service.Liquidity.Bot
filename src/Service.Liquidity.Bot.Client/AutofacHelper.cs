﻿using Autofac;

// ReSharper disable UnusedMember.Global

namespace Service.Liquidity.Bot.Client
{
    public static class AutofacHelper
    {
        public static void RegisterLiquidityBotClient(this ContainerBuilder builder, string grpcServiceUrl)
        {
            var factory = new LiquidityBotClientFactory(grpcServiceUrl);
        }
    }
}
