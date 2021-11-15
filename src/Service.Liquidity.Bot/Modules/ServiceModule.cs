using Autofac;
using Service.Liquidity.Bot.Job;
using Service.Liquidity.Bot.Services;
using Telegram.Bot;

namespace Service.Liquidity.Bot.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PortfolioTradeNotificator>()
                .As<IStartable>()
                .AutoActivate()
                .SingleInstance();
            
            builder.RegisterType<PortfolioStatusHistoryManager>()
                .AsSelf()
                .SingleInstance();

            builder
                .RegisterType<AssetPortfolioStatusNotificator>()
                .As<IStartable>()
                .AutoActivate()
                .SingleInstance();
            
            builder
                .RegisterType<PortfolioSettlementNotificator>()
                .As<IStartable>()
                .AutoActivate()
                .SingleInstance();

            builder
                .RegisterInstance(new TelegramBotClient(Program.Settings.BotApiKey))
                .As<ITelegramBotClient>()
                .SingleInstance();
        }
    }
}