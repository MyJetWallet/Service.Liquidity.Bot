using Autofac;
using MyJetWallet.Sdk.NoSql;
using Service.Liquidity.Monitoring.Domain.Models;

namespace Service.Liquidity.Bot.Modules
{
    public class NoSqlModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var noSqlClient = builder.CreateNoSqlClient(Program.ReloadedSettings(e => e.MyNoSqlReaderHostPort));
            builder.RegisterMyNoSqlReader<AssetPortfolioStatusNoSql>(noSqlClient, AssetPortfolioStatusNoSql.TableName);
        }
    }
}