using System;
using System.Collections.Generic;
using System.Linq;
using Service.Liquidity.Monitoring.Domain.Models;

namespace Service.Liquidity.Bot.Services
{
    public class PortfolioStatusHistoryManager
    {
        public Dictionary<string, AssetPortfolioStatus> StatusHistory { get; set; } = new Dictionary<string, AssetPortfolioStatus>();
        public List<StatusTelegramMessage> MessageHistory { get; set; } = new List<StatusTelegramMessage>();
        
        public void AddToMessageHistory(StatusTelegramMessage message)
        {
            MessageHistory.Add(message);
        }

        public List<StatusTelegramMessage> GetPnlMessageFromHistory()
        {
            return MessageHistory.Where(e => e.Type == StatusTelegramMessageType.PNL).ToList();
        }
        
        public List<StatusTelegramMessage> GetNetUsdMessages()
        {
            return MessageHistory.Where(e => e.Type == StatusTelegramMessageType.NETUSD).ToList();
        }
        
        public void AddStatusToHistory(AssetPortfolioStatus status)
        {
            StatusHistory[status.Asset] = status;
        }

        public AssetPortfolioStatus GetLastStatusFromHistory(string asset)
        {
            return StatusHistory.TryGetValue(asset, out var result) ? result : null;
        }
    }

    public class StatusTelegramMessage
    {
        public string Asset { get; set; }
        public StatusTelegramMessageType Type { get; set; }
        public decimal Limit { get; set; }
        public decimal Volume { get; set; }
        public DateTime PublishDate { get; set; }
        public string MessageText { get; set; }

        public StatusTelegramMessage(string asset, StatusTelegramMessageType type, decimal limit, decimal volume, DateTime publishDate, string messageText)
        {
            Asset = asset;
            Type = type;
            Limit = limit;
            Volume = volume;
            PublishDate = publishDate;
            MessageText = messageText;
        }
    }

    public enum StatusTelegramMessageType
    {
        PNL,
        NETUSD
    }
}