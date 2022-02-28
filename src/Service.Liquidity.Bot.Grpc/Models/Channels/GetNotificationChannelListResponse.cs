using System.Collections.Generic;
using System.Runtime.Serialization;
using Service.Liquidity.Bot.Domain.Models;

namespace Service.Liquidity.Bot.Grpc.Models.Channels
{
    [DataContract]
    public class GetNotificationChannelListResponse
    {
        [DataMember(Order = 1)] public IEnumerable<NotificationChannel> Items { get; set; }
    }
}