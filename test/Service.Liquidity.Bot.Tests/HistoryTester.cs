using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NUnit.Framework;
using Service.Liquidity.Bot.Job;
using Service.Liquidity.Bot.Services;
using Service.Liquidity.Bot.Tests.Mock;
using Service.Liquidity.Monitoring.Domain.Models;

namespace Service.Liquidity.Bot.Tests
{
    public class HistoryTester
    {
        private AssetPortfolioStatusNotificator _assetPortfolioStatusNotificator;
        private ILoggerFactory _loggerFactory;
        private PortfolioStatusHistoryManager _manager;
        
        [SetUp]
        public void Setup()
        {
            
            _loggerFactory =
                LoggerFactory.Create(builder =>
                    builder.AddSimpleConsole(options =>
                    {
                        options.IncludeScopes = true;
                        options.SingleLine = true;
                        options.TimestampFormat = "hh:mm:ss ";
                    }));

            var logger = _loggerFactory.CreateLogger<AssetPortfolioStatusNotificator>();
            _manager = new PortfolioStatusHistoryManager();
            var bot = new ITelegramBotClientMock();
            var dataReader = new IMyNoSqlServerDataReaderMock();

            _assetPortfolioStatusNotificator = new AssetPortfolioStatusNotificator(logger, dataReader, _manager, bot, "-12312", 5);
        }

        //[Test]
        // public void Test1()
        // {
        //     var statuses = new List<AssetPortfolioStatusNoSql>();
        //
        //     var status1 = new AssetPortfolioStatusNoSql()
        //     {
        //         AssetStatus = new AssetPortfolioStatus()
        //         {
        //             Asset = "BTC",
        //             NetUsdStrike = 200,
        //             NetUsd = 240,
        //             UplStrike = 0,
        //             Upl = 0,
        //             UpdateDate = new DateTime(2021, 8, 16, 10, 40, 0)
        //         }
        //     };
        //     var status2 = new AssetPortfolioStatusNoSql()
        //     {
        //         AssetStatus = new AssetPortfolioStatus()
        //         {
        //             Asset = "BTC",
        //             NetUsdStrike = 100,
        //             NetUsd = 150,
        //             UplStrike = 0,
        //             Upl = 0,
        //             UpdateDate = new DateTime(2021, 8, 16, 10, 41, 0)
        //         }
        //     };
        //     statuses.Add(status1);
        //     statuses.Add(status2);
        //     
        //     _assetPortfolioStatusNotificator.HandleUpdate(statuses);
        //     
        //     Console.WriteLine(JsonConvert.SerializeObject(_manager.StatusHistory));
        //     Console.WriteLine(JsonConvert.SerializeObject(_manager.MessageHistory));
        //
        //     Assert.AreEqual(1, _manager.StatusHistory.Count);
        //     Assert.AreEqual(1, _manager.MessageHistory.Count);
        //     
        //     Assert.AreEqual(status2.AssetStatus.NetUsd, _manager.StatusHistory.FirstOrDefault().Value.NetUsd);
        // }
        
        //[Test]
        // public void Test2()
        // {
        //     var statuses = new List<AssetPortfolioStatusNoSql>();
        //
        //     var status1 = new AssetPortfolioStatusNoSql()
        //     {
        //         AssetStatus = new AssetPortfolioStatus()
        //         {
        //             Asset = "BTC",
        //             NetUsdStrike = 200,
        //             NetUsd = 240,
        //             UplStrike = 0,
        //             Upl = 0,
        //             UpdateDate = new DateTime(2021, 8, 16, 10, 40, 0)
        //         }
        //     };
        //     var status2 = new AssetPortfolioStatusNoSql()
        //     {
        //         AssetStatus = new AssetPortfolioStatus()
        //         {
        //             Asset = "BTC",
        //             NetUsdStrike = 100,
        //             NetUsd = 150,
        //             UplStrike = 0,
        //             Upl = 0,
        //             UpdateDate = new DateTime(2021, 8, 16, 10, 41, 0)
        //         }
        //     };
        //     var status3 = new AssetPortfolioStatusNoSql()
        //     {
        //         AssetStatus = new AssetPortfolioStatus()
        //         {
        //             Asset = "BTC",
        //             NetUsdStrike = 200,
        //             NetUsd = 250,
        //             UplStrike = 0,
        //             Upl = 0,
        //             UpdateDate = new DateTime(2021, 8, 16, 10, 44, 0)
        //         }
        //     };
        //     statuses.Add(status1);
        //     statuses.Add(status2);
        //     statuses.Add(status3);
        //     
        //     _assetPortfolioStatusNotificator.HandleUpdate(statuses);
        //     
        //     Console.WriteLine(JsonConvert.SerializeObject(_manager.StatusHistory));
        //     Console.WriteLine(JsonConvert.SerializeObject(_manager.MessageHistory));
        //
        //     Assert.AreEqual(1, _manager.StatusHistory.Count);
        //     Assert.AreEqual(1, _manager.MessageHistory.Count);
        //     
        //     Assert.AreEqual(status3.AssetStatus.NetUsd, _manager.StatusHistory.FirstOrDefault().Value.NetUsd);
        // }
        
        //[Test]
        // public void Test2_1()
        // {
        //     var statuses = new List<AssetPortfolioStatusNoSql>();
        //
        //     var status1 = new AssetPortfolioStatusNoSql()
        //     {
        //         AssetStatus = new AssetPortfolioStatus()
        //         {
        //             Asset = "BTC",
        //             NetUsdStrike = 200,
        //             NetUsd = 240,
        //             UplStrike = 0,
        //             Upl = 0,
        //             UpdateDate = new DateTime(2021, 8, 16, 10, 40, 0)
        //         }
        //     };
        //     var status2 = new AssetPortfolioStatusNoSql()
        //     {
        //         AssetStatus = new AssetPortfolioStatus()
        //         {
        //             Asset = "BTC",
        //             NetUsdStrike = 100,
        //             NetUsd = 150,
        //             UplStrike = 0,
        //             Upl = 0,
        //             UpdateDate = new DateTime(2021, 8, 16, 10, 41, 0)
        //         }
        //     };
        //     var status3 = new AssetPortfolioStatusNoSql()
        //     {
        //         AssetStatus = new AssetPortfolioStatus()
        //         {
        //             Asset = "BTC",
        //             NetUsdStrike = 200,
        //             NetUsd = 250,
        //             UplStrike = 0,
        //             Upl = 0,
        //             UpdateDate = new DateTime(2021, 8, 16, 10, 46, 0)
        //         }
        //     };
        //     statuses.Add(status1);
        //     statuses.Add(status2);
        //     statuses.Add(status3);
        //     
        //     _assetPortfolioStatusNotificator.HandleUpdate(statuses);
        //     
        //     Console.WriteLine(JsonConvert.SerializeObject(_manager.StatusHistory));
        //     Console.WriteLine(JsonConvert.SerializeObject(_manager.MessageHistory));
        //
        //     Assert.AreEqual(1, _manager.StatusHistory.Count);
        //     Assert.AreEqual(1, _manager.MessageHistory.Count);
        //     
        //     Assert.AreEqual(status3.AssetStatus.NetUsd, _manager.StatusHistory.FirstOrDefault().Value.NetUsd);
        // }
    }
}
