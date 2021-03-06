using Service.Liquidity.Bot.Domain.Models;
using Service.Liquidity.Monitoring.Domain.Models.Checks;
using Service.Liquidity.Monitoring.Domain.Models.Rules;

namespace Service.Liquidity.Bot.Domain.Extensions;

public static class MonitoringRuleExtensions
{
    public static TimeSpan NotificationRemindPeriod { get; set; } = TimeSpan.FromHours(4);
    
    public static bool NeedsNotification(this MonitoringRule rule, DateTime? lastNotificationDate)
    {
        lastNotificationDate ??= DateTime.MinValue;
        var action = new SendNotificationMonitoringAction();

        if (rule.ActionsByTypeName == null || !rule.ActionsByTypeName.TryGetValue(action.TypeName, out _))
        {
            return false;
        }

        if (rule.CurrentState == null)
        {
            return false;
        }

        if (rule.CurrentState.IsActive != rule.PrevState?.IsActive)
        {
            return true;
        }

        var timeToRemind = DateTime.UtcNow - lastNotificationDate > NotificationRemindPeriod;

        if (rule.CurrentState.IsActive && timeToRemind)
        {
            return true;
        }

        return false;
    }

    public static string GetNotificationText(this MonitoringRule rule)
    {
        var ruleChecks = rule.Checks ?? new List<PortfolioCheck>();
        var title =
            $"Rule <b>{rule.Name}</b> is {(rule.CurrentState.IsActive ? "active" : "inactive")}:{Environment.NewLine}{rule.Description}";
        var checkDescriptions = ruleChecks.Select(ch => ch.GenerateCheckDescription());
        var body = string.Join($"{Environment.NewLine}", checkDescriptions);

        return $"{title}{Environment.NewLine}" +
               $"{body}{Environment.NewLine}{Environment.NewLine}" +
               $"Date: {rule.CurrentState.Date:yyyy-MM-dd hh:mm:ss}";
    }
}