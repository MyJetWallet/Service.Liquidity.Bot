using System;
using System.Collections.Generic;
using Autofac;
using Microsoft.Extensions.Logging;
using MyNoSqlServer.Abstractions;
using Newtonsoft.Json;
using Service.Liquidity.Bot.Services;
using Service.Liquidity.Monitoring.Domain.Models;
using Telegram.Bot;

namespace Service.Liquidity.Bot.Job
{
    public class AssetPortfolioStatusNotificator : IStartable
    {
        private readonly ILogger<AssetPortfolioStatusNotificator> _logger;
        private readonly ITelegramBotClient _botApiClient;
        private readonly IMyNoSqlServerDataReader<AssetPortfolioStatusNoSql> _myNoSqlServerDataReader;
        private readonly PortfolioStatusHistoryManager _portfolioStatusHistoryManager;

        public AssetPortfolioStatusNotificator(ILogger<AssetPortfolioStatusNotificator> logger,
            IMyNoSqlServerDataReader<AssetPortfolioStatusNoSql> myNoSqlServerDataReader,
            PortfolioStatusHistoryManager portfolioStatusHistoryManager)
        {
            _logger = logger;
            _myNoSqlServerDataReader = myNoSqlServerDataReader;
            _portfolioStatusHistoryManager = portfolioStatusHistoryManager;
            _botApiClient = new TelegramBotClient(Program.Settings.BotApiKey);
        }

        public void Start()
        {
            _myNoSqlServerDataReader.SubscribeToUpdateEvents(HandleUpdate, HandleDelete);
        }
        
        private void HandleUpdate(IReadOnlyList<AssetPortfolioStatusNoSql> statuses)
        {
            foreach (var statusNoSql in statuses)
            {
                var status = statusNoSql.AssetStatus;
                
                var changed = CheckIsChanged(status);
                if (changed)
                {
                    _portfolioStatusHistoryManager.AddStatusToHistory(status);
                    
                    var canPushMessage = CheckPushHistory(status);
                    if (canPushMessage)
                    {
                        _portfolioStatusHistoryManager.AddToMessageHistory(status);

                        var message = JsonConvert.SerializeObject(status, Formatting.Indented);
                        _botApiClient.SendTextMessageAsync(Program.Settings.ChatId, message).GetAwaiter().GetResult();
                    }
                }
            }
        }

        private bool CheckIsChanged(AssetPortfolioStatus status)
        {
            var lastStatus = _portfolioStatusHistoryManager.GetLastStatusFromHistory(status.Asset);
            
            if (lastStatus == null)
            {
                return true;
            }
            if (lastStatus.UplStrike != status.UplStrike ||
                lastStatus.NetUsdStrike != status.NetUsdStrike)
            {
                return true;
            }
            return false;
        }

        private bool CheckPushHistory(AssetPortfolioStatus status)
        {

            if (status.UplStrike == 0 || status.NetUsdStrike == 0)
                return false;
            
            var lastMessage = _portfolioStatusHistoryManager.GetMessageFromHistory(status.Asset);
            
            if (lastMessage == null)
            {
                return true;
            }
            if (lastMessage.UplStrike == status.UplStrike &&
                lastMessage.NetUsdStrike == status.NetUsdStrike)
            {
                return false;
            }
            if (lastMessage.UpdateDate.AddMinutes(Program.Settings.PortfolioStatusTimeoutInMin) > DateTime.UtcNow)
            {
                return false;
            }
            return true;
        }

        private void HandleDelete(IReadOnlyList<AssetPortfolioStatusNoSql> statuses)
        {
            _logger.LogInformation("Handle Delete message from AssetPortfolioStatusNoSql");
        }
    }
}