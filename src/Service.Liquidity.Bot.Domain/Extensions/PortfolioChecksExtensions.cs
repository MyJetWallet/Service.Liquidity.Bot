﻿using Humanizer;
using Service.Liquidity.Monitoring.Domain.Models.Checks;

namespace Service.Liquidity.Bot.Domain.Extensions;

public static class PortfolioChecksExtensions
{
    public static string GenerateCheckDescription(this PortfolioCheck check)
    {
        const string inactiveSymbol = "\U0001F7E2"; // green circle
        const string activeSymbol = "\U0001F534"; // red circle

        var title = check.CurrentState.IsActive
            ? $"{activeSymbol} Check <b>{check.Name}</b> is active"
            : $"{inactiveSymbol} Check <b>{check.Name}</b> is inactive";

        return $"{title}{Environment.NewLine}" +
               $"{check.MetricType.Humanize()} is {check.OperatorType.Humanize()} {check.TargetValue}" +
               $"Metric value: <b>{check.CurrentState.MetricValue}</b>{Environment.NewLine}" +
               $"Target value: <b>{check.TargetValue}</b>";
    }

    public static string GetOrGenerateDescription(this PortfolioCheck check)
    {
        if (string.IsNullOrWhiteSpace(check.Description))
        {
            return check.GenerateCheckDescription();
        }

        return
            $"Check {check.Name} is {(check.CurrentState.IsActive ? "active" : "inactive")}:{Environment.NewLine}{check.Description}";
    }
}