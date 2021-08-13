using System.Collections.Generic;
using Service.Liquidity.Monitoring.Domain.Models;

namespace Service.Liquidity.Bot.Services
{
    public class PortfolioStatusHistoryManager
    {
        private Dictionary<string, AssetPortfolioStatus> StatusHistory { get; set; } = new Dictionary<string, AssetPortfolioStatus>();
        private Dictionary<string, AssetPortfolioStatus> MessageHistory { get; set; } = new Dictionary<string, AssetPortfolioStatus>();
        
        public void AddToMessageHistory(AssetPortfolioStatus status)
        {
            MessageHistory[status.Asset] = status;
        }

        public AssetPortfolioStatus GetMessageFromHistory(string asset)
        {
            return MessageHistory.TryGetValue(asset, out var result) ? result : null;
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
}