using System;
using MyNoSqlServer.Abstractions;
using Service.Liquidity.Bot.Domain.Models;

namespace Service.Liquidity.Bot.NoSql
{
    public class NotificationChannelNoSql : MyNoSqlDbEntity
    {
        public const string TableName = "myjetwallet-liquidity-bot-notification-channels";
        public static string GeneratePartitionKey() => "*";
        public static string GenerateRowKey(string id) => id;

        public NotificationChannel Value { get; set; }

        public static NotificationChannelNoSql Create(NotificationChannel src)
        {
            return new NotificationChannelNoSql
            {
                PartitionKey = GeneratePartitionKey(),
                RowKey = GenerateRowKey(src.Id ?? Guid.NewGuid().ToString()),
                Value = src
            };
        }
    }
}