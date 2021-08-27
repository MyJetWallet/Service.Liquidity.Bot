using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly string _chatId;
        private readonly int _timeoutInMin;

        public AssetPortfolioStatusNotificator(ILogger<AssetPortfolioStatusNotificator> logger,
            IMyNoSqlServerDataReader<AssetPortfolioStatusNoSql> myNoSqlServerDataReader,
            PortfolioStatusHistoryManager portfolioStatusHistoryManager,
            ITelegramBotClient botApiClient,
            string chatId = "",
            int timeoutInMin = 0)
        {
            _logger = logger;
            _myNoSqlServerDataReader = myNoSqlServerDataReader;
            _portfolioStatusHistoryManager = portfolioStatusHistoryManager;
            _botApiClient = botApiClient;

            _chatId = string.IsNullOrWhiteSpace(chatId)
                ? Program.Settings.ChatId
                : chatId;
            _timeoutInMin = timeoutInMin == 0
                ? Program.Settings.PortfolioStatusTimeoutInMin
                : timeoutInMin;
        }

        public void Start()
        {
            _myNoSqlServerDataReader.SubscribeToUpdateEvents(HandleUpdate, HandleDelete);
        }
        
        public void HandleUpdate(IReadOnlyList<AssetPortfolioStatusNoSql> statuses)
        {
            foreach (var statusNoSql in statuses)
            {
                var status = statusNoSql.AssetStatus;
                
                var changed = CheckIsChanged(status);
                if (changed)
                {
                    _portfolioStatusHistoryManager.AddStatusToHistory(status);
                    
                    var pnlMessage = GetPnlPushMessage(status);
                    var netUsdMessage = GetNetUsdPushMessage(status);
                    if (pnlMessage != null)
                    {
                        _portfolioStatusHistoryManager.AddToMessageHistory(pnlMessage);
                        _botApiClient.SendTextMessageAsync(_chatId, pnlMessage.MessageText).GetAwaiter().GetResult();
                    }
                    if (netUsdMessage != null)
                    {
                        _portfolioStatusHistoryManager.AddToMessageHistory(netUsdMessage);
                        _botApiClient.SendTextMessageAsync(_chatId, netUsdMessage.MessageText).GetAwaiter().GetResult();
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

        private StatusTelegramMessage GetPnlPushMessage(AssetPortfolioStatus status)
        {
            var lastPnlMessages = _portfolioStatusHistoryManager.GetPnlMessageFromHistory();

            var lastPnlMessage =
                lastPnlMessages.FirstOrDefault(e => e.Asset == status.Asset && e.Limit == status.UplStrike);
            
            var messageText = GetMessageText(status, StatusTelegramMessageType.PNL);
            var statusMessage = new StatusTelegramMessage(status.Asset, StatusTelegramMessageType.PNL, status.UplStrike,
                status.Upl, status.UpdateDate, messageText);

            if (lastPnlMessage == null)
            {
                if (status.UplStrike != 0)
                {
                    return statusMessage;
                }
                return null;
            }
            
            if (lastPnlMessage.PublishDate.AddMinutes(_timeoutInMin) < DateTime.UtcNow)
            {
                return statusMessage;
            }
            return null;
        }
        
        private StatusTelegramMessage GetNetUsdPushMessage(AssetPortfolioStatus status)
        {
            var lastNetUsdMessages = _portfolioStatusHistoryManager.GetNetUsdMessages();

            var lastNetUsdMessage =
                lastNetUsdMessages.FirstOrDefault(e => e.Asset == status.Asset && e.Limit == status.NetUsdStrike);
            
            var messageText = GetMessageText(status, StatusTelegramMessageType.NETUSD);
            var statusMessage = new StatusTelegramMessage(status.Asset, StatusTelegramMessageType.NETUSD, status.NetUsdStrike,
                status.NetUsd, status.UpdateDate, messageText);
            
            if (lastNetUsdMessage == null)
            {
                if (status.NetUsdStrike != 0)
                {
                    return statusMessage;
                }
                return null;
            }
            
            if (lastNetUsdMessage.PublishDate.AddMinutes(_timeoutInMin) < DateTime.UtcNow)
            {
                return statusMessage;
            }
            return null;
        }

        private string GetMessageText(AssetPortfolioStatus status, StatusTelegramMessageType type)
        {
            switch (type)
            {
                case StatusTelegramMessageType.PNL:
                    return $"[ALERT] Hit {status.Asset} Unrealize PL = ${Math.Round(status.Upl, 2)}, Limit = ${status.UplStrike}.";
                case StatusTelegramMessageType.NETUSD:
                    return $"[ALERT] Hit {status.Asset} NetUSD = ${Math.Round(status.NetUsd, 2)}, Limit = ${status.NetUsdStrike}.";
                default:
                    _logger.LogError($"GetMessageText fail with status: {JsonConvert.SerializeObject(status)} and type: {type}");
                    return string.Empty;
            }
        }

        private void HandleDelete(IReadOnlyList<AssetPortfolioStatusNoSql> statuses)
        {
            _logger.LogInformation("Handle Delete message from AssetPortfolioStatusNoSql");
        }
    }
}