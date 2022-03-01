using Autofac;
using Service.Liquidity.Bot.Domain;
using Service.Liquidity.Bot.NoSql;
using Service.Liquidity.Bot.Subscribers;
using Telegram.Bot;

namespace Service.Liquidity.Bot.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterInstance(new TelegramBotClient(Program.Settings.BotApiKey))
                .As<ITelegramBotClient>()
                .SingleInstance();
            builder.RegisterType<AssetPortfolioStatusSubscriber>()
                .SingleInstance()
                .AutoActivate();
            builder.RegisterType<SendNotificationCommandSubscriber>()
                .SingleInstance()
                .AutoActivate();
            builder.RegisterType<NotificationChannelsNoSqlRepository>()
                .As<INotificationChannelsRepository>()
                .SingleInstance()
                .AutoActivate();
        }
    }
}