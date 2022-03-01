using Autofac;
using MyJetWallet.Sdk.ServiceBus;
using MyServiceBus.Abstractions;
using Service.Liquidity.Bot.Domain.Models;
using Service.Liquidity.Bot.Subscribers;
using Service.Liquidity.Monitoring.Domain.Models;
using Service.Liquidity.Portfolio.Domain.Models;

namespace Service.Liquidity.Bot.Modules
{
    public class ServiceBusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var serviceBusClient = builder.RegisterMyServiceBusTcpClient(Program.ReloadedSettings(e => e.SpotServiceBusHostPort), Program.LogFactory);
            
            // builder.RegisterMyServiceBusSubscriberBatch<AssetPortfolioTrade>(serviceBusClient, 
            //     AssetPortfolioTrade.TopicName, 
            //     "Liquidity-Bot",
            //     TopicQueueType.PermanentWithSingleConnection);
                
            // builder.RegisterMyServiceBusSubscriberBatch<ManualSettlement>(serviceBusClient, 
            //     ManualSettlement.TopicName, 
            //     "Liquidity-Bot",
            //     TopicQueueType.PermanentWithSingleConnection);

            var queueName = "Liquidity-Bot";
            builder.RegisterMyServiceBusSubscriberSingle<AssetPortfolioStatusMessage>(serviceBusClient, 
                AssetPortfolioStatusMessage.TopicName, 
                queueName, 
                TopicQueueType.PermanentWithSingleConnection);
            builder.RegisterMyServiceBusSubscriberSingle<SendNotificationCommand>(serviceBusClient, 
                SendNotificationCommand.SbTopicName, 
                queueName, 
                TopicQueueType.DeleteOnDisconnect);
        }
    }
}