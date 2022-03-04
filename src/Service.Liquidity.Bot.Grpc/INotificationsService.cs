using System.ServiceModel;
using System.Threading.Tasks;
using Service.Liquidity.Bot.Grpc.Models.Notifications;

namespace Service.Liquidity.Bot.Grpc;

[ServiceContract]
public interface INotificationsService
{
    [OperationContract]
    public Task<SendNotificationResponse> SendAsync(SendNotificationRequest request);
}