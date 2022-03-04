using System.Runtime.Serialization;

namespace Service.Liquidity.Bot.Grpc.Models.Notifications
{
    [DataContract]
    public class SendNotificationResponse
    {
        [DataMember(Order = 1)] public bool IsError { get; set; }
        [DataMember(Order = 2)] public string ErrorMessage { get; set; }
    }
}