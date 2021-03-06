using Autofac;
using MyJetWallet.Sdk.NoSql;
using Service.Liquidity.Bot.NoSql;

namespace Service.Liquidity.Bot.Modules
{
    public class NoSqlModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterMyNoSqlWriter<NotificationChannelNoSql>(() => Program.Settings.MyNoSqlWriterUrl,
                NotificationChannelNoSql.TableName);
            builder.RegisterMyNoSqlWriter<NotificationNoSql>(() => Program.Settings.MyNoSqlWriterUrl,
                NotificationNoSql.TableName);
        }
    }
}