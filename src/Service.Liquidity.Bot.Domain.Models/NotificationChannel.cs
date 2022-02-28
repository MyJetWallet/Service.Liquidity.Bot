using System.Runtime.Serialization;

namespace Service.Liquidity.Bot.Domain.Models
{
    [DataContract]
    public class NotificationChannel
    {
        [DataMember(Order = 1)] public string Id { get; set; }
        [DataMember(Order = 2)] public string Name { get; set; }
        [DataMember(Order = 3)] public string ChatId { get; set; }
    }
}