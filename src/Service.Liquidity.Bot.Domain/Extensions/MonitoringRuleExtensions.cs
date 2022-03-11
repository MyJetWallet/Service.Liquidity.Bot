using Service.Liquidity.Monitoring.Domain.Models.Checks;
using Service.Liquidity.Monitoring.Domain.Models.RuleSets;

namespace Service.Liquidity.Bot.Domain.Extensions;

public static class MonitoringRuleExtensions
{
    public static bool NeedsNotification(this MonitoringRule rule, DateTime? lastNotificationDate)
    {
        if (string.IsNullOrWhiteSpace(rule.NotificationChannelId))
        {
            return false;
        }

        if (rule.CurrentState.IsActive != rule.PrevState.IsActive)
        {
            return true;
        }

        var timeToRemind = DateTime.UtcNow - lastNotificationDate > TimeSpan.FromMinutes(60);

        if (lastNotificationDate == null || timeToRemind)
        {
            return true;
        }

        return false;
    }

    public static string GetNotificationText(this MonitoringRule rule, IEnumerable<PortfolioCheck> checks)
    {
        var checkIds = rule.CheckIds.ToHashSet();
        var ruleChecks = checks
            .Where(ch => checkIds.Contains(ch.Id))
            .ToList();
        var title =
            $"Rule <b>{rule.Name}</b> is {(rule.CurrentState.IsActive ? "active" : "inactive")}:{Environment.NewLine}{rule.Description}";
        var checkDescriptions = ruleChecks.Select(ch => ch.GetOrGenerateDescription());
        var body = string.Join($"{Environment.NewLine}", checkDescriptions);

        return $"{title}{Environment.NewLine}" +
               $"{body}{Environment.NewLine}{Environment.NewLine}" +
               $"Date: {rule.CurrentState.Date:yyyy-MM-dd hh:mm:ss}";
    }
}