using System.Collections.Generic;
using Autofac;
using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.Service;
using MyServiceBus.Abstractions;
using MyServiceBus.TcpClient;
using Service.Liquidity.Bot.Services;
using Service.Liquidity.Engine.Client.ServiceBus;
using Service.Liquidity.Engine.Domain.Models.Portfolio;

namespace Service.Liquidity.Bot.Modules
{
    public class ServiceModule : Module
    {
        private static ILogger ServiceBusLogger { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            ServiceBusLogger = Program.LogFactory.CreateLogger(nameof(MyServiceBusTcpClient));


            var s = Program.Settings.SpotServiceBusHostPort;

            var serviceBusClient = new MyServiceBusTcpClient(Program.ReloadedSettings(e => e.SpotServiceBusHostPort), ApplicationEnvironment.HostName);
            serviceBusClient.Log.AddLogException(ex => ServiceBusLogger.LogInformation(ex, "Exception in MyServiceBusTcpClient"));
            serviceBusClient.Log.AddLogInfo(info => ServiceBusLogger.LogDebug($"MyServiceBusTcpClient[info]: {info}"));
            serviceBusClient.SocketLogs.AddLogInfo((context, msg) => ServiceBusLogger.LogInformation($"MyServiceBusTcpClient[Socket {context?.Id}|{context?.ContextName}|{context?.Inited}][Info] {msg}"));
            serviceBusClient.SocketLogs.AddLogException((context, exception) => ServiceBusLogger.LogInformation(exception, $"MyServiceBusTcpClient[Socket {context?.Id}|{context?.ContextName}|{context?.Inited}][Exception] {exception.Message}"));



            builder.RegisterInstance(serviceBusClient).AsSelf().SingleInstance();

            var queryName = "Liquidity-Bot";


            builder
                .RegisterInstance(new PositionPortfolioSubscriber(serviceBusClient, queryName, TopicQueueType.Permanent))
                .As<ISubscriber<IReadOnlyList<PositionPortfolio>>>()
                .SingleInstance();

            builder.RegisterType<PositionPortfolioNotificatorJob>()
                .AsSelf()
                .AutoActivate()
                .SingleInstance();

            serviceBusClient.Start();
            ;
        }
    }
}