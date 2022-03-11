using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyNoSqlServer.Abstractions;
using Service.Liquidity.Bot.Domain;
using Service.Liquidity.Bot.Domain.Interfaces;
using Service.Liquidity.Bot.Domain.Models;

namespace Service.Liquidity.Bot.NoSql
{
    public class NotificationChannelsNoSqlRepository : INotificationChannelsRepository
    {
        private readonly IMyNoSqlServerDataWriter<NotificationChannelNoSql> _myNoSqlServerDataWriter;

        public NotificationChannelsNoSqlRepository(
            IMyNoSqlServerDataWriter<NotificationChannelNoSql> myNoSqlServerDataWriter
        )
        {
            _myNoSqlServerDataWriter = myNoSqlServerDataWriter;
        }

        public async Task AddOrUpdateAsync(NotificationChannel model)
        {
            var nosqlModel = NotificationChannelNoSql.Create(model);
            await _myNoSqlServerDataWriter.InsertOrReplaceAsync(nosqlModel);
        }

        public async Task<IEnumerable<NotificationChannel>> GetAsync()
        {
            var models = await _myNoSqlServerDataWriter.GetAsync();

            return models.Select(m => m.Value);
        }

        public async Task<NotificationChannel> GetAsync(string id)
        {
            var model = await _myNoSqlServerDataWriter.GetAsync(NotificationChannelNoSql.GeneratePartitionKey(),
                NotificationChannelNoSql.GenerateRowKey(id));

            return model?.Value;
        }

        public async Task DeleteAsync(string id)
        {
            await _myNoSqlServerDataWriter.DeleteAsync(NotificationChannelNoSql.GeneratePartitionKey(),
                NotificationChannelNoSql.GenerateRowKey(id));
        }
    }
}