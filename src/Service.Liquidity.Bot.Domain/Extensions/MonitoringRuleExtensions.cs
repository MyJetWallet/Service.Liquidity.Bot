using Service.Liquidity.Bot.Domain.Models;
using Service.Liquidity.Monitoring.Domain.Models.Checks;
using Service.Liquidity.Monitoring.Domain.Models.RuleSets;
using Service.Liquidity.Monitoring.Domain.Models.RuleSets.Actions;

namespace Service.Liquidity.Bot.Domain.Extensions;

public static class MonitoringRuleExtensions
{
    public static bool NeedsNotification(this MonitoringRule rule, DateTime? lastNotificationDate)
    {
        var channelId = rule.ParamsByName?.Values
            .FirstOrDefault(p => p.Name == MonitoringRuleConsts.ChannelIdParam)?
            .GetString();
        if (string.IsNullOrWhiteSpace(rule.NotificationChannelId) && string.IsNullOrWhiteSpace(channelId))
        {
            return false;
        }

        if (rule.CurrentState.IsActive != rule.PrevState.IsActive)
        {
            return true;
        }

        var timeToRemind = DateTime.UtcNow - lastNotificationDate > TimeSpan.FromMinutes(60);

        if (rule.CurrentState.IsActive && (lastNotificationDate == null || timeToRemind))
        {
            return true;
        }

        return false;
    }

    public static string GetNotificationText(this MonitoringRule rule, IEnumerable<PortfolioCheck>? checks = null)
    {
        var checkIds = rule.CheckIds?.ToHashSet() ?? new HashSet<string>();
        var ruleChecks = checkIds.Any()
            ? checks?.Where(ch => checkIds.Contains(ch.Id))
                .ToList() ?? new List<PortfolioCheck>()
            : rule.Checks ?? new List<PortfolioCheck>();
        var title =
            $"Rule <b>{rule.Name}</b> is {(rule.CurrentState.IsActive ? "active" : "inactive")}:{Environment.NewLine}{rule.Description}";
        var checkDescriptions = ruleChecks.Select(ch => ch.GetOrGenerateDescription());
        var body = string.Join($"{Environment.NewLine}", checkDescriptions);

        return $"{title}{Environment.NewLine}" +
               $"{body}{Environment.NewLine}{Environment.NewLine}" +
               $"Date: {rule.CurrentState.Date:yyyy-MM-dd hh:mm:ss}";
    }
}