using System.Runtime.Serialization;

namespace Service.Liquidity.Bot.Grpc.Models.Channels
{
    [DataContract]
    public class GetNotificationChannelRequest
    {
        [DataMember(Order = 1)] public string Id { get; set; }
    }
}