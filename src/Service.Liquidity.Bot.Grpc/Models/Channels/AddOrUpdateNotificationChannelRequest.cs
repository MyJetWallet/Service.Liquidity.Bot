using System.Runtime.Serialization;
using Service.Liquidity.Bot.Domain.Models;

namespace Service.Liquidity.Bot.Grpc.Models.Channels
{
    [DataContract]
    public class AddOrUpdateNotificationChannelRequest
    {
        [DataMember(Order = 1)] public NotificationChannel Item { get; set; }
    }
}