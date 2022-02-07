using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.NoSql;
using MyJetWallet.Sdk.Service;
using MyJetWallet.Sdk.ServiceBus;

namespace Service.Liquidity.Bot
{
    public class ApplicationLifetimeManager : ApplicationLifetimeManagerBase
    {
        private readonly ILogger<ApplicationLifetimeManager> _logger;
        private readonly ServiceBusLifeTime _myServiceBusTcpClientLifeTime;
        //private readonly MyNoSqlClientLifeTime _myNoSqlTcpClientLifeTime;

        public ApplicationLifetimeManager(IHostApplicationLifetime appLifetime,
            ILogger<ApplicationLifetimeManager> logger,
            ServiceBusLifeTime myServiceBusTcpClientLifeTime
            //MyNoSqlClientLifeTime myNoSqlTcpClientLifeTime
         )
            : base(appLifetime)
        {
            _logger = logger;
            _myServiceBusTcpClientLifeTime = myServiceBusTcpClientLifeTime;
            //_myNoSqlTcpClientLifeTime = myNoSqlTcpClientLifeTime;
        }

        protected override void OnStarted()
        {
            _logger.LogInformation("OnStarted has been called.");
            //_myNoSqlTcpClientLifeTime.Start();
            _myServiceBusTcpClientLifeTime.Start();
        }

        protected override void OnStopping()
        {
            _logger.LogInformation("OnStopping has been called.");
            _myServiceBusTcpClientLifeTime.Stop();
            //_myNoSqlTcpClientLifeTime.Stop();
        }

        protected override void OnStopped()
        {
            _logger.LogInformation("OnStopped has been called.");
        }
    }
}
