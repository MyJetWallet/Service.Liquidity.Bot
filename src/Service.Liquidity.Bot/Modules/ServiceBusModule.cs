using Autofac;
using MyJetWallet.Sdk.ServiceBus;
using MyServiceBus.Abstractions;
using Service.IntrestManager.Domain.Models;
using Service.Liquidity.Hedger.Domain.Models;
using Service.Liquidity.Monitoring.Domain.Models;

namespace Service.Liquidity.Bot.Modules
{
    public class ServiceBusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var serviceBusClient = builder.RegisterMyServiceBusTcpClient(Program.ReloadedSettings(e => e.SpotServiceBusHostPort), Program.LogFactory);

            var queueName = "Liquidity-Bot";
            builder.RegisterMyServiceBusSubscriberSingle<AssetPortfolioStatusMessage>(serviceBusClient, 
                AssetPortfolioStatusMessage.TopicName, 
                queueName, 
                TopicQueueType.PermanentWithSingleConnection);
            builder.RegisterMyServiceBusSubscriberSingle<PortfolioMonitoringMessage>(serviceBusClient, 
                PortfolioMonitoringMessage.TopicName, 
                queueName, 
                TopicQueueType.DeleteOnDisconnect);
            builder.RegisterMyServiceBusSubscriberSingle<HedgeOperation>(serviceBusClient, 
                HedgeOperation.TopicName, 
                queueName, 
                TopicQueueType.DeleteOnDisconnect);
            builder.RegisterMyServiceBusSubscriberBatch<FailedInterestRateMessage>(serviceBusClient, 
                FailedInterestRateMessage.TopicName, 
                queueName, 
                TopicQueueType.DeleteOnDisconnect);
            builder.RegisterMyServiceBusSubscriberSingle<InterestProcessingResult>(serviceBusClient, 
                InterestProcessingResult.TopicName, 
                queueName, 
                TopicQueueType.DeleteOnDisconnect);
        }
    }
}