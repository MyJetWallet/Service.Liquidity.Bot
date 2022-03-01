using System.Runtime.Serialization;

namespace Service.Liquidity.Bot.Domain.Models
{
    [DataContract]
    public class SendNotificationCommand
    {
        public const string SbTopicName = "jetwallet-liquidity-bot-send-notification-command";

        [DataMember(Order = 1)] public string ChannelId { get; set; }
        [DataMember(Order = 2)] public string Message { get; set; }
    }
}