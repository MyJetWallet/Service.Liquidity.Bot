using System;
using System.Threading.Tasks;
using MyNoSqlServer.Abstractions;
using Service.Liquidity.Bot.Domain.Interfaces;

namespace Service.Liquidity.Bot.NoSql;

public class NotificationsNoSqlCache : INotificationsCache
{
    private readonly IMyNoSqlServerDataWriter<NotificationNoSql> _myNoSqlServerDataWriter;

    public NotificationsNoSqlCache(
        IMyNoSqlServerDataWriter<NotificationNoSql> myNoSqlServerDataWriter
        )
    {
        _myNoSqlServerDataWriter = myNoSqlServerDataWriter;
    }

    public async Task AddOrUpdateAsync(string ruleId, DateTime expires)
    {
        var nosqlModel = NotificationNoSql.Create(ruleId, expires);
        await _myNoSqlServerDataWriter.InsertOrReplaceAsync(nosqlModel);
        await _myNoSqlServerDataWriter.CleanAndKeepMaxRecords(NotificationNoSql.GeneratePartitionKey(), 3500);
    }

    public async Task<DateTime> GetLastNotificationDateAsync(string ruleId)
    {
        var model = await _myNoSqlServerDataWriter.GetAsync(NotificationNoSql.GeneratePartitionKey(),
            NotificationNoSql.GenerateRowKey(ruleId));

        return model?.CreatedDate ?? DateTime.MinValue;
    }
}