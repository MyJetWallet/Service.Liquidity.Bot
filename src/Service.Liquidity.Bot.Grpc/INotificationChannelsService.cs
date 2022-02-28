using System.ServiceModel;
using System.Threading.Tasks;
using Service.Liquidity.Bot.Grpc.Models.Channels;

namespace Service.Liquidity.Bot.Grpc
{
    [ServiceContract]
    public interface INotificationChannelsService
    {
        [OperationContract]
        Task<GetNotificationChannelListResponse> GetListAsync(GetNotificationChannelListRequest request);

        [OperationContract]
        Task<AddOrUpdateNotificationChannelResponse> AddOrUpdateAsync(AddOrUpdateNotificationChannelRequest request);

        [OperationContract]
        Task<GetNotificationChannelResponse> GetAsync(GetNotificationChannelRequest request);

        [OperationContract]
        Task<DeleteNotificationChannelResponse> DeleteAsync(DeleteNotificationChannelRequest request);
    }
}