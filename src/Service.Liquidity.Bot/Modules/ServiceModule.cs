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
    public class ServiceModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var serviceBusLogger = Program.LogFactory.CreateLogger(nameof(MyServiceBusTcpClient));

            var serviceBusClient = new MyServiceBusTcpClient(Program.ReloadedSettings(e => e.SpotServiceBusHostPort), ApplicationEnvironment.HostName);
            serviceBusClient.Log.AddLogException(ex => serviceBusLogger.LogInformation(ex, "Exception in MyServiceBusTcpClient"));
            serviceBusClient.Log.AddLogInfo(info => serviceBusLogger.LogDebug($"MyServiceBusTcpClient[info]: {info}"));
            serviceBusClient.SocketLogs.AddLogInfo((context, msg) => serviceBusLogger.LogInformation($"MyServiceBusTcpClient[Socket {context?.Id}|{context?.ContextName}|{context?.Inited}][Info] {msg}"));
            serviceBusClient.SocketLogs.AddLogException((context, exception) => serviceBusLogger.LogInformation(exception, $"MyServiceBusTcpClient[Socket {context?.Id}|{context?.ContextName}|{context?.Inited}][Exception] {exception.Message}"));

            builder.RegisterInstance(serviceBusClient).AsSelf().SingleInstance();

            var queryName = "Liquidity-Bot";
            ISubscriber<IReadOnlyList<PositionPortfolio>> subscriber = new PositionPortfolioSubscriber(serviceBusClient, queryName,
                TopicQueueType.PermanentWithSingleConnection);

            builder
                .RegisterInstance(subscriber)
                .As<ISubscriber<IReadOnlyList<PositionPortfolio>>>()
                .SingleInstance();

            builder.RegisterType<PositionPortfolioNotificatorJob>()
                .AutoActivate()
                .SingleInstance();
        }
    }
}