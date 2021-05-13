using System.Runtime.Serialization;
using Service.Liquidity.Bot.Domain.Models;

namespace Service.Liquidity.Bot.Grpc.Models
{
    [DataContract]
    public class HelloMessage : IHelloMessage
    {
        [DataMember(Order = 1)]
        public string Message { get; set; }
    }
}