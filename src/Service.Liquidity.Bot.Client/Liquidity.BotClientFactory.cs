﻿using System;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using JetBrains.Annotations;
using MyJetWallet.Sdk.GrpcMetrics;
using ProtoBuf.Grpc.Client;
using Service.Liquidity.Bot.Grpc;

namespace Service.Liquidity.Bot.Client
{
    [UsedImplicitly]
    public class LiquidityBotClientFactory
    {
        private readonly CallInvoker _channel;

        public LiquidityBotClientFactory(string assetsDictionaryGrpcServiceUrl)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            var channel = GrpcChannel.ForAddress(assetsDictionaryGrpcServiceUrl);
            _channel = channel.Intercept(new PrometheusMetricsInterceptor());
        }

        public IHelloService GetHelloService() => _channel.CreateGrpcService<IHelloService>();
    }
}
