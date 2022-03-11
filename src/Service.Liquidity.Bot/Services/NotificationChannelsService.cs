using System.Threading.Tasks;
using Service.Liquidity.Bot.Domain;
using Service.Liquidity.Bot.Domain.Interfaces;
using Service.Liquidity.Bot.Grpc;
using Service.Liquidity.Bot.Grpc.Models.Channels;

namespace Service.Liquidity.Bot.Services
{
    public class NotificationChannelsService : INotificationChannelsService
    {
        private readonly INotificationChannelsRepository _notificationChannelsRepository;

        public NotificationChannelsService(
            INotificationChannelsRepository notificationChannelsRepository
        )
        {
            _notificationChannelsRepository = notificationChannelsRepository;
        }

        public async Task<GetNotificationChannelListResponse> GetListAsync(GetNotificationChannelListRequest request)
        {
            var items = await _notificationChannelsRepository.GetAsync();

            return new GetNotificationChannelListResponse
            {
                Items = items
            };
        }

        public async Task<AddOrUpdateNotificationChannelResponse> AddOrUpdateAsync(
            AddOrUpdateNotificationChannelRequest request)
        {
            await _notificationChannelsRepository.AddOrUpdateAsync(request.Item);

            return new AddOrUpdateNotificationChannelResponse();
        }

        public async Task<GetNotificationChannelResponse> GetAsync(GetNotificationChannelRequest request)
        {
            var item = await _notificationChannelsRepository.GetAsync(request.Id);

            return new GetNotificationChannelResponse
            {
                Item = item
            };
        }

        public async Task<DeleteNotificationChannelResponse> DeleteAsync(DeleteNotificationChannelRequest request)
        {
            await _notificationChannelsRepository.DeleteAsync(request.Id);

            return new DeleteNotificationChannelResponse();
        }
    }
}