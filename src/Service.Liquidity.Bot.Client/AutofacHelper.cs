using Autofac;
using Service.Liquidity.Bot.Grpc;

// ReSharper disable UnusedMember.Global

namespace Service.Liquidity.Bot.Client
{
    public static class AutofacHelper
    {
        public static void RegisterLiquidityBotClient(this ContainerBuilder builder, string grpcServiceUrl)
        {
            var factory = new LiquidityBotClientFactory(grpcServiceUrl);
        }

        public static void RegisterLiquidityNotificationChannelsClient(this ContainerBuilder builder, string grpcServiceUrl)
        {
            var factory = new LiquidityBotClientFactory(grpcServiceUrl);
            builder.RegisterInstance(factory.GetNotificationChannelsService()).As<INotificationChannelsService>()
                .SingleInstance();
        }
    }
}