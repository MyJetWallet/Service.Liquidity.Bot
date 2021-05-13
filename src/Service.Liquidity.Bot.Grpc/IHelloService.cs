using System.ServiceModel;
using System.Threading.Tasks;
using Service.Liquidity.Bot.Grpc.Models;

namespace Service.Liquidity.Bot.Grpc
{
    [ServiceContract]
    public interface IHelloService
    {
        [OperationContract]
        Task<HelloMessage> SayHelloAsync(HelloRequest request);
    }
}