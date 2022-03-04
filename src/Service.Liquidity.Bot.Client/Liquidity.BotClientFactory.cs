using System;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using JetBrains.Annotations;
using MyJetWallet.Sdk.Grpc;
using MyJetWallet.Sdk.GrpcMetrics;
using Service.Liquidity.Bot.Grpc;

namespace Service.Liquidity.Bot.Client
{
    [UsedImplicitly]
    public class LiquidityBotClientFactory : MyGrpcClientFactory
    {
        private readonly CallInvoker _channel;

        public LiquidityBotClientFactory(string grpcServiceUrl) : base(grpcServiceUrl)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            var channel = GrpcChannel.ForAddress(grpcServiceUrl);
            _channel = channel.Intercept(new PrometheusMetricsInterceptor());
        }
        
        public INotificationChannelsService GetNotificationChannelsService() => CreateGrpcService<INotificationChannelsService>();
        public INotificationsService GetNotificationsService() => CreateGrpcService<INotificationsService>();

    }
}
