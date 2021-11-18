using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.Payments;
using Telegram.Bot.Types.ReplyMarkups;
using File = Telegram.Bot.Types.File;

namespace Service.Liquidity.Bot.Tests.Mock
{
    public class ITelegramBotClientMock : ITelegramBotClient
    {
        public Task<TResponse> MakeRequestAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<bool> TestApiAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void StartReceiving(UpdateType[] allowedUpdates = null, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void StopReceiving()
        {
            throw new NotImplementedException();
        }

        public Task<Update[]> GetUpdatesAsync(int offset = 0, int limit = 0, int timeout = 0, IEnumerable<UpdateType> allowedUpdates = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task SetWebhookAsync(string url, InputFileStream certificate = null, string ipAddress = null, int maxConnections = 0,
            IEnumerable<UpdateType> allowedUpdates = null, bool dropPendingUpdates = false,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task DeleteWebhookAsync(bool dropPendingUpdates = false, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<WebhookInfo> GetWebhookInfoAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<User> GetMeAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task LogOutAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task CloseAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public async Task<Message> SendTextMessageAsync(ChatId chatId, string text, ParseMode parseMode = ParseMode.Markdown, IEnumerable<MessageEntity> entities = null,
            bool disableWebPagePreview = false, bool disableNotification = false, int replyToMessageId = 0,
            bool allowSendingWithoutReply = false, IReplyMarkup replyMarkup = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return new Message() {};
        }

        public Task<Message> ForwardMessageAsync(ChatId chatId, ChatId fromChatId, int messageId, bool disableNotification = false,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<MessageId> CopyMessageAsync(ChatId chatId, ChatId fromChatId, int messageId, string caption = null,
            ParseMode parseMode = ParseMode.Markdown, IEnumerable<MessageEntity> captionEntities = null, int replyToMessageId = 0,
            bool disableNotification = false, IReplyMarkup replyMarkup = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<Message> SendPhotoAsync(ChatId chatId, InputOnlineFile photo, string caption = null, ParseMode parseMode = ParseMode.Markdown,
            IEnumerable<MessageEntity> captionEntities = null, bool disableNotification = false, int replyToMessageId = 0,
            bool allowSendingWithoutReply = false, IReplyMarkup replyMarkup = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<Message> SendAudioAsync(ChatId chatId, InputOnlineFile audio, string caption = null, ParseMode parseMode = ParseMode.Markdown,
            IEnumerable<MessageEntity> captionEntities = null, int duration = 0, string performer = null, string title = null,
            InputMedia thumb = null, bool disableNotification = false, int replyToMessageId = 0,
            bool allowSendingWithoutReply = false, IReplyMarkup replyMarkup = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<Message> SendDocumentAsync(ChatId chatId, InputOnlineFile document, InputMedia thumb = null, string caption = null,
            ParseMode parseMode = ParseMode.Markdown, IEnumerable<MessageEntity> captionEntities = null, bool disableContentTypeDetection = false,
            bool disableNotification = false, int replyToMessageId = 0, bool allowSendingWithoutReply = false,
            IReplyMarkup replyMarkup = null, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<Message> SendStickerAsync(ChatId chatId, InputOnlineFile sticker, bool disableNotification = false,
            int replyToMessageId = 0, bool allowSendingWithoutReply = false, IReplyMarkup replyMarkup = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<Message> SendVideoAsync(ChatId chatId, InputOnlineFile video, int duration = 0, int width = 0, int height = 0,
            InputMedia thumb = null, string caption = null, ParseMode parseMode = ParseMode.Markdown, IEnumerable<MessageEntity> captionEntities = null,
            bool supportsStreaming = false, bool disableNotification = false, int replyToMessageId = 0,
            bool allowSendingWithoutReply = false, IReplyMarkup replyMarkup = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<Message> SendAnimationAsync(ChatId chatId, InputOnlineFile animation, int duration = 0, int width = 0, int height = 0,
            InputMedia thumb = null, string caption = null, ParseMode parseMode = ParseMode.Markdown, IEnumerable<MessageEntity> captionEntities = null,
            bool disableNotification = false, int replyToMessageId = 0, bool allowSendingWithoutReply = false,
            IReplyMarkup replyMarkup = null, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<Message> SendVoiceAsync(ChatId chatId, InputOnlineFile voice, string caption = null, ParseMode parseMode = ParseMode.Markdown,
            IEnumerable<MessageEntity> captionEntities = null, int duration = 0, bool disableNotification = false, int replyToMessageId = 0,
            bool allowSendingWithoutReply = false, IReplyMarkup replyMarkup = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<Message> SendVideoNoteAsync(ChatId chatId, InputTelegramFile videoNote, int duration = 0, int length = 0,
            InputMedia thumb = null, bool disableNotification = false, int replyToMessageId = 0,
            bool allowSendingWithoutReply = false, IReplyMarkup replyMarkup = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<Message[]> SendMediaGroupAsync(ChatId chatId, IEnumerable<IAlbumInputMedia> media, bool disableNotification = false, int replyToMessageId = 0,
            bool allowSendingWithoutReply = false, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<Message> SendLocationAsync(ChatId chatId, float latitude, float longitude, int livePeriod = 0, int heading = 0,
            int proximityAlertRadius = 0, bool disableNotification = false, int replyToMessageId = 0,
            bool allowSendingWithoutReply = false, IReplyMarkup replyMarkup = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<Message> SendVenueAsync(ChatId chatId, float latitude, float longitude, string title, string address,
            string foursquareId = null, string foursquareType = null, string googlePlaceId = null,
            string googlePlaceType = null, bool disableNotification = false, int replyToMessageId = 0,
            bool allowSendingWithoutReply = false, IReplyMarkup replyMarkup = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<Message> SendContactAsync(ChatId chatId, string phoneNumber, string firstName, string lastName = null, string vCard = null,
            bool disableNotification = false, int replyToMessageId = 0, bool allowSendingWithoutReply = false,
            IReplyMarkup replyMarkup = null, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<Message> SendPollAsync(ChatId chatId, string question, IEnumerable<string> options, bool? isAnonymous = null, PollType? type = null,
            bool? allowsMultipleAnswers = null, int? correctOptionId = null, string explanation = null,
            ParseMode explanationParseMode = ParseMode.Markdown, IEnumerable<MessageEntity> explanationEntities = null, int? openPeriod = null,
            DateTime? closeDate = null, bool? isClosed = null, bool disableNotification = false, int replyToMessageId = 0,
            bool allowSendingWithoutReply = false, IReplyMarkup replyMarkup = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<Message> SendDiceAsync(ChatId chatId, Emoji? emoji = null, bool disableNotification = false, int replyToMessageId = 0,
            bool allowSendingWithoutReply = false, IReplyMarkup replyMarkup = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task SendChatActionAsync(ChatId chatId, ChatAction chatAction,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<UserProfilePhotos> GetUserProfilePhotosAsync(long userId, int offset = 0, int limit = 0,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<File> GetFileAsync(string fileId, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task DownloadFileAsync(string filePath, Stream destination,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<File> GetInfoAndDownloadFileAsync(string fileId, Stream destination,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task KickChatMemberAsync(ChatId chatId, long userId, DateTime untilDate = new DateTime(), bool? revokeMessages = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task LeaveChatAsync(ChatId chatId, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task UnbanChatMemberAsync(ChatId chatId, long userId, bool onlyIfBanned = false,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<Chat> GetChatAsync(ChatId chatId, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<ChatMember[]> GetChatAdministratorsAsync(ChatId chatId, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<int> GetChatMembersCountAsync(ChatId chatId, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<ChatMember> GetChatMemberAsync(ChatId chatId, long userId, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task AnswerCallbackQueryAsync(string callbackQueryId, string text = null, bool showAlert = false, string url = null,
            int cacheTime = 0, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task RestrictChatMemberAsync(ChatId chatId, long userId, ChatPermissions permissions,
            DateTime untilDate = new DateTime(), CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task PromoteChatMemberAsync(ChatId chatId, long userId, bool? isAnonymous = null, bool? canManageChat = null,
            bool? canChangeInfo = null, bool? canPostMessages = null, bool? canEditMessages = null,
            bool? canDeleteMessages = null, bool? canManageVoiceChats = null, bool? canInviteUsers = null,
            bool? canRestrictMembers = null, bool? canPinMessages = null, bool? canPromoteMembers = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task SetChatAdministratorCustomTitleAsync(ChatId chatId, long userId, string customTitle,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task SetChatPermissionsAsync(ChatId chatId, ChatPermissions permissions,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<BotCommand[]> GetMyCommandsAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task SetMyCommandsAsync(IEnumerable<BotCommand> commands, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<Message> EditMessageTextAsync(ChatId chatId, int messageId, string text, ParseMode parseMode = ParseMode.Markdown,
            IEnumerable<MessageEntity> entities = null, bool disableWebPagePreview = false, InlineKeyboardMarkup replyMarkup = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task EditMessageTextAsync(string inlineMessageId, string text, ParseMode parseMode = ParseMode.Markdown,
            IEnumerable<MessageEntity> entities = null, bool disableWebPagePreview = false, InlineKeyboardMarkup replyMarkup = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<Message> StopMessageLiveLocationAsync(ChatId chatId, int messageId, InlineKeyboardMarkup replyMarkup = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task StopMessageLiveLocationAsync(string inlineMessageId, InlineKeyboardMarkup replyMarkup = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<Message> EditMessageCaptionAsync(ChatId chatId, int messageId, string caption, ParseMode parseMode = ParseMode.Markdown,
            IEnumerable<MessageEntity> captionEntities = null, InlineKeyboardMarkup replyMarkup = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task EditMessageCaptionAsync(string inlineMessageId, string caption, ParseMode parseMode = ParseMode.Markdown,
            IEnumerable<MessageEntity> captionEntities = null, InlineKeyboardMarkup replyMarkup = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<Message> EditMessageMediaAsync(ChatId chatId, int messageId, InputMediaBase media, InlineKeyboardMarkup replyMarkup = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task EditMessageMediaAsync(string inlineMessageId, InputMediaBase media, InlineKeyboardMarkup replyMarkup = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<Message> EditMessageReplyMarkupAsync(ChatId chatId, int messageId, InlineKeyboardMarkup replyMarkup = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task EditMessageReplyMarkupAsync(string inlineMessageId, InlineKeyboardMarkup replyMarkup = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<Message> EditMessageLiveLocationAsync(ChatId chatId, int messageId, float latitude, float longitude,
            float horizontalAccuracy = 0, int heading = 0, int proximityAlertRadius = 0,
            InlineKeyboardMarkup replyMarkup = null, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task EditMessageLiveLocationAsync(string inlineMessageId, float latitude, float longitude, float horizontalAccuracy = 0,
            int heading = 0, int proximityAlertRadius = 0, InlineKeyboardMarkup replyMarkup = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<Poll> StopPollAsync(ChatId chatId, int messageId, InlineKeyboardMarkup replyMarkup = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task DeleteMessageAsync(ChatId chatId, int messageId, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }


        public Task<Message> SendInvoiceAsync(long chatId, string title, string description, string payload, string providerToken,
            string currency, IEnumerable<LabeledPrice> prices, int maxTipAmount = 0, int[] suggestedTipAmounts = null,
            string startParameter = null, string providerData = null, string photoUrl = null, int photoSize = 0,
            int photoWidth = 0, int photoHeight = 0, bool needName = false, bool needPhoneNumber = false,
            bool needEmail = false, bool needShippingAddress = false, bool sendPhoneNumberToProvider = false,
            bool sendEmailToProvider = false, bool isFlexible = false, bool disableNotification = false,
            int replyToMessageId = 0, bool allowSendingWithoutReply = false, InlineKeyboardMarkup replyMarkup = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task AnswerShippingQueryAsync(string shippingQueryId, IEnumerable<ShippingOption> shippingOptions,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task AnswerShippingQueryAsync(string shippingQueryId, string errorMessage,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task AnswerPreCheckoutQueryAsync(string preCheckoutQueryId,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task AnswerPreCheckoutQueryAsync(string preCheckoutQueryId, string errorMessage,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<Message> SendGameAsync(long chatId, string gameShortName, bool disableNotification = false, int replyToMessageId = 0,
            bool allowSendingWithoutReply = false, InlineKeyboardMarkup replyMarkup = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<Message> SetGameScoreAsync(long userId, int score, long chatId, int messageId, bool force = false,
            bool disableEditMessage = false, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task SetGameScoreAsync(long userId, int score, string inlineMessageId, bool force = false,
            bool disableEditMessage = false, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<GameHighScore[]> GetGameHighScoresAsync(long userId, long chatId, int messageId,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<GameHighScore[]> GetGameHighScoresAsync(long userId, string inlineMessageId,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StickerSet> GetStickerSetAsync(string name, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<File> UploadStickerFileAsync(long userId, InputFileStream pngSticker,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task CreateNewStickerSetAsync(long userId, string name, string title, InputOnlineFile pngSticker, string emojis,
            bool isMasks = false, MaskPosition maskPosition = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task AddStickerToSetAsync(long userId, string name, InputOnlineFile pngSticker, string emojis,
            MaskPosition maskPosition = null, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task CreateNewAnimatedStickerSetAsync(long userId, string name, string title, InputFileStream tgsSticker, string emojis,
            bool isMasks = false, MaskPosition maskPosition = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task AddAnimatedStickerToSetAsync(long userId, string name, InputFileStream tgsSticker, string emojis,
            MaskPosition maskPosition = null, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task SetStickerPositionInSetAsync(string sticker, int position,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task DeleteStickerFromSetAsync(string sticker, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task SetStickerSetThumbAsync(string name, long userId, InputOnlineFile thumb = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<string> ExportChatInviteLinkAsync(ChatId chatId, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task SetChatPhotoAsync(ChatId chatId, InputFileStream photo,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task DeleteChatPhotoAsync(ChatId chatId, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task SetChatTitleAsync(ChatId chatId, string title, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task SetChatDescriptionAsync(ChatId chatId, string description = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task PinChatMessageAsync(ChatId chatId, int messageId, bool disableNotification = false,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task UnpinChatMessageAsync(ChatId chatId, int messageId = 0,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task UnpinAllChatMessages(ChatId chatId, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task SetChatStickerSetAsync(ChatId chatId, string stickerSetName,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task DeleteChatStickerSetAsync(ChatId chatId, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<ChatInviteLink> CreateChatInviteLinkAsync(ChatId chatId, DateTime? expireDate = null, int? memberLimit = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<ChatInviteLink> EditChatInviteLinkAsync(ChatId chatId, string inviteLink, DateTime? expireDate = null, int? memberLimit = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<ChatInviteLink> RevokeChatInviteLinkAsync(ChatId chatId, string inviteLink,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public long? BotId { get; }
        public TimeSpan Timeout { get; set; }
        public IExceptionParser ExceptionsParser { get; set; }
        public bool IsReceiving { get; }
        public int MessageOffset { get; set; }
        public event AsyncEventHandler<ApiRequestEventArgs> OnMakingApiRequest;
        public event AsyncEventHandler<ApiResponseEventArgs> OnApiResponseReceived;
    }
}