using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.Service;
using MyNoSqlServer.DataReader;
using MyServiceBus.TcpClient;

namespace Service.Liquidity.Bot
{
    public class ApplicationLifetimeManager : ApplicationLifetimeManagerBase
    {
        private readonly ILogger<ApplicationLifetimeManager> _logger;
        private readonly MyServiceBusTcpClient _myServiceBusTcpClient;
        private readonly MyNoSqlTcpClient[] _myNoSqlTcpClientManagers;

        public ApplicationLifetimeManager(IHostApplicationLifetime appLifetime,
            ILogger<ApplicationLifetimeManager> logger,
            MyServiceBusTcpClient myServiceBusTcpClient,
            MyNoSqlTcpClient[] myNoSqlTcpClientManagers)
            : base(appLifetime)
        {
            _logger = logger;
            _myServiceBusTcpClient = myServiceBusTcpClient;
            _myNoSqlTcpClientManagers = myNoSqlTcpClientManagers;
        }

        protected override void OnStarted()
        {
            _logger.LogInformation("OnStarted has been called.");
            foreach(var client in _myNoSqlTcpClientManagers)
            {
                client.Start();
            }
            _myServiceBusTcpClient.Start();
        }

        protected override void OnStopping()
        {
            _logger.LogInformation("OnStopping has been called.");
            _myServiceBusTcpClient.Stop();
            foreach(var client in _myNoSqlTcpClientManagers)
            {
                try
                {
                    client.Stop();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        protected override void OnStopped()
        {
            _logger.LogInformation("OnStopped has been called.");
        }
    }
}
