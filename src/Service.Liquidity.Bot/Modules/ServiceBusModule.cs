using Autofac;
using MyJetWallet.Sdk.Service;
using MyJetWallet.Sdk.ServiceBus;
using MyServiceBus.Abstractions;
using MyServiceBus.TcpClient;
using Service.Liquidity.Portfolio.Domain.Models;

namespace Service.Liquidity.Bot.Modules
{
    public class ServiceBusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var serviceBusClient = new MyServiceBusTcpClient(Program.ReloadedSettings(e => e.SpotServiceBusHostPort), ApplicationEnvironment.HostName);

            builder.RegisterInstance(serviceBusClient).AsSelf().SingleInstance();
            
            builder.RegisterMyServiceBusSubscriberBatch<AssetPortfolioTrade>(serviceBusClient, AssetPortfolioTrade.TopicName, 
                "Liquidity-Bot",
                TopicQueueType.PermanentWithSingleConnection);
                
            builder.RegisterMyServiceBusSubscriberBatch<ManualSettlement>(serviceBusClient, ManualSettlement.TopicName, 
                "Liquidity-Bot",
                TopicQueueType.PermanentWithSingleConnection);
        }
    }
}