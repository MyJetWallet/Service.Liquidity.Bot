using System;
using System.Collections.Generic;
using MyNoSqlServer.Abstractions;
using Service.Liquidity.Monitoring.Domain.Models;

namespace Service.Liquidity.Bot.Tests.Mock
{
    public class IMyNoSqlServerDataReaderMock : IMyNoSqlServerDataReader<AssetPortfolioStatusNoSql>
    {
        public AssetPortfolioStatusNoSql Get(string partitionKey, string rowKey)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<AssetPortfolioStatusNoSql> Get(string partitionKey)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<AssetPortfolioStatusNoSql> Get(string partitionKey, int skip, int take)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<AssetPortfolioStatusNoSql> Get(string partitionKey, int skip, int take, Func<AssetPortfolioStatusNoSql, bool> condition)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<AssetPortfolioStatusNoSql> Get(string partitionKey, Func<AssetPortfolioStatusNoSql, bool> condition)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<AssetPortfolioStatusNoSql> Get(Func<AssetPortfolioStatusNoSql, bool> condition = null)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public int Count(string partitionKey)
        {
            throw new NotImplementedException();
        }

        public int Count(string partitionKey, Func<AssetPortfolioStatusNoSql, bool> condition)
        {
            throw new NotImplementedException();
        }

        public IMyNoSqlServerDataReader<AssetPortfolioStatusNoSql> SubscribeToUpdateEvents(Action<IReadOnlyList<AssetPortfolioStatusNoSql>> updateSubscriber, Action<IReadOnlyList<AssetPortfolioStatusNoSql>> deleteSubscriber)
        {
            throw new NotImplementedException();
        }
    }
}