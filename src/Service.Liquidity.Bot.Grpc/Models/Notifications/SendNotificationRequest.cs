using System.Runtime.Serialization;

namespace Service.Liquidity.Bot.Grpc.Models.Notifications;

[DataContract]
public class SendNotificationRequest
{
    [DataMember(Order = 1)] public string ChannelId { get; set; }
    [DataMember(Order = 2)] public string Text { get; set; }
}