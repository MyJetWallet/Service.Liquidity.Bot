using Autofac;
using MyJetWallet.Sdk.Service;
using MyJetWallet.Sdk.ServiceBus;
using MyServiceBus.Abstractions;
using MyServiceBus.TcpClient;
using Service.Liquidity.Bot.Job;
using Service.Liquidity.Bot.Services;
using Service.Liquidity.Engine.Domain.Models.Portfolio;
using Service.Liquidity.Portfolio.Domain.Models;

namespace Service.Liquidity.Bot.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PortfolioTradeNotificator>()
                .AsSelf()
                .SingleInstance();
            
            builder.RegisterType<PortfolioStatusHistoryManager>()
                .AsSelf()
                .SingleInstance();

            builder
                .RegisterType<AssetPortfolioStatusNotificator>()
                .As<IStartable>()
                .AutoActivate()
                .SingleInstance();
        }
    }
}