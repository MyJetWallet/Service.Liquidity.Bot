using Autofac;
using MyJetWallet.Sdk.ServiceBus;
using MyServiceBus.Abstractions;
using Service.IntrestManager.Domain.Models;
using Service.Liquidity.Alerts.Domain.Models.Alerts;
using Service.Liquidity.Monitoring.Domain.Models;

namespace Service.Liquidity.Bot.Modules
{
    public class ServiceBusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var serviceBusClient =
                builder.RegisterMyServiceBusTcpClient(() => Program.Settings.SpotServiceBusHostPort,
                    Program.LogFactory);

            var queueName = "Liquidity-Bot";
            builder.RegisterMyServiceBusSubscriberSingle<PortfolioMonitoringMessage>(serviceBusClient, 
                PortfolioMonitoringMessage.TopicName, 
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
            builder.RegisterMyServiceBusSubscriberSingle<NewAlertMessage>(serviceBusClient, 
                NewAlertMessage.TopicName, 
                queueName, 
                TopicQueueType.DeleteOnDisconnect);
        }
    }
}