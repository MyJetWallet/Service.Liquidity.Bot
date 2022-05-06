using Autofac;
using Service.Liquidity.Bot.Domain.Interfaces;
using Service.Liquidity.Bot.NoSql;
using Service.Liquidity.Bot.Services;
using Service.Liquidity.Bot.Subscribers;
using Telegram.Bot;

namespace Service.Liquidity.Bot.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(new TelegramBotClient(Program.Settings.BotApiKey)).As<ITelegramBotClient>()
                .SingleInstance();
            builder.RegisterType<AssetPortfolioStatusSubscriber>().SingleInstance().AutoActivate();
            builder.RegisterType<PortfolioMonitoringMessageSubscriber>().As<IStartable>()
                .SingleInstance().AutoActivate();
            builder.RegisterType<HedgeOperationsSubscriber>().As<IStartable>()
                .SingleInstance().AutoActivate();
            builder.RegisterType<NotificationChannelsNoSqlRepository>().As<INotificationChannelsRepository>()
                .SingleInstance().AutoActivate();
            builder.RegisterType<NotificationTelegramSender>().As<INotificationSender>()
                .SingleInstance().AutoActivate();
            builder.RegisterType<NotificationsNoSqlCache>().As<INotificationsCache>()
                .SingleInstance().AutoActivate();
            builder.RegisterType<NewAlertMessageSubscriber>().As<IStartable>()
                .SingleInstance().AutoActivate();
        }
    }
}