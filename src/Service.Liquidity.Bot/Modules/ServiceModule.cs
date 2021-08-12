using Autofac;
using MyJetWallet.Sdk.Service;
using MyJetWallet.Sdk.ServiceBus;
using MyServiceBus.Abstractions;
using MyServiceBus.TcpClient;
using Service.Liquidity.Bot.Services;
using Service.Liquidity.Engine.Domain.Models.Portfolio;
using Service.Liquidity.Portfolio.Domain.Models;

namespace Service.Liquidity.Bot.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var serviceBusClient = new MyServiceBusTcpClient(Program.ReloadedSettings(e => e.SpotServiceBusHostPort), ApplicationEnvironment.HostName);

            builder.RegisterInstance(serviceBusClient).AsSelf().SingleInstance();

            var queueName = "Liquidity-Bot";

            builder.RegisterMyServiceBusSubscriberBatch<PositionPortfolio>(serviceBusClient, PositionPortfolio.TopicName, 
                queueName,
                TopicQueueType.Permanent);
            
            builder.RegisterMyServiceBusSubscriberBatch<AssetPortfolioTrade>(serviceBusClient, AssetPortfolioTrade.TopicName, 
                queueName,
                TopicQueueType.Permanent);
            
            builder.RegisterType<PositionPortfolioNotificatorJob>()
                .AsSelf()
                .AutoActivate()
                .SingleInstance();
            
            builder.RegisterType<PortfolioTradeNotificatorJob>()
                .AsSelf()
                .AutoActivate()
                .SingleInstance();
        }
    }
}