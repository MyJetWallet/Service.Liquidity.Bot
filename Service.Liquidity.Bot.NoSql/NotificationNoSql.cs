using System;
using MyNoSqlServer.Abstractions;

namespace Service.Liquidity.Bot.NoSql;

public class NotificationNoSql : MyNoSqlDbEntity
{
    public const string TableName = "myjetwallet-liquidity-sent-notifications-cache";
    public static string GeneratePartitionKey() => "*";
    public static string GenerateRowKey(string id) => id;

    public string RuleId { get; set; }
    public DateTime CreatedDate { get; set; }

    public static NotificationNoSql Create(string ruleId, DateTime expires)
    {
        return new NotificationNoSql
        {
            PartitionKey = GeneratePartitionKey(),
            RowKey = GenerateRowKey(ruleId ?? Guid.NewGuid().ToString()),
            RuleId = ruleId,
            Expires = expires,
            CreatedDate = DateTime.UtcNow
        };
    }
}