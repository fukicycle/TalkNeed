using Chat.SampleApp.Models;
using Newtonsoft.Json;

namespace Chat.SampleApp.Services
{
    public sealed class ChatRoomService : IChatRoomService
    {
        private const string RESOURCE_NAME = "chat_rooms_1";
        private readonly IFirebaseService _firebaseService;
        public ChatRoomService(IFirebaseService firebaseService)
        {
            _firebaseService = firebaseService;
        }
        public async Task AddUserToChatRoomAsync(Guid userId, Guid chatId)
        {
            var user = await _firebaseService.GetAsync<User>("users_1", userId.ToString());
            IEnumerable<Guid> chatRoomList = user.ChatRoomList.Append(chatId);
            var updateUser = new User(user.Id, user.Nickname, user.FCMToken, chatRoomList, user.MessageList);
            await _firebaseService.UpdateAsync(updateUser, "users_1", userId.ToString());
        }

        public async IAsyncEnumerable<ChatRoom> GetChatRoomListAsync(Guid userId)
        {
            User user = await _firebaseService.GetAsync<User>("users_1", userId.ToString());
            foreach (var chatId in user.ChatRoomList)
            {
                yield return await _firebaseService.GetAsync<ChatRoom>(RESOURCE_NAME, chatId.ToString());
            }
        }

        public async Task<Guid> RegisterAsync(string title)
        {
            ChatRoom chatRoom = new ChatRoom(Guid.NewGuid(), title, string.Empty, DateTime.Now, Enumerable.Empty<Guid>());
            return await _firebaseService.AddAsync(chatRoom, RESOURCE_NAME);
        }
    }
}
