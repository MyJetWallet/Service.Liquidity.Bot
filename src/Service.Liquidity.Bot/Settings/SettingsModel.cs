using MyJetWallet.Sdk.Service;
using MyYamlParser;

namespace Service.Liquidity.Bot.Settings
{
    public class SettingsModel
    {
        [YamlProperty("LiquidityBot.SeqServiceUrl")]
        public string SeqServiceUrl { get; set; }

        [YamlProperty("LiquidityBot.SpotServiceBusHostPort")]
        public string SpotServiceBusHostPort { get; set; }

        [YamlProperty("LiquidityBot.ZipkinUrl")]
        public string ZipkinUrl { get; set; }

        [YamlProperty("LiquidityBot.ElkLogs")]
        public LogElkSettings ElkLogs { get; set; }

        [YamlProperty("LiquidityBot.BotApiKey")]
        public string BotApiKey { get; set; }

        [YamlProperty("LiquidityBot.ChatId")]
        public string ChatId { get; set; }
        
        // [YamlProperty("LiquidityBot.PortfolioStatusTimeoutInMin")]
        // public int PortfolioStatusTimeoutInMin { get; set; }
        //
        // [YamlProperty("LiquidityBot.MyNoSqlReaderHostPort")]
        // public string MyNoSqlReaderHostPort { get; set; }
    }
}
