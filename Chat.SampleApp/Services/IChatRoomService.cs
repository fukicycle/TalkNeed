using Chat.SampleApp.Models;

namespace Chat.SampleApp.Services
{
    public interface IChatRoomService
    {
        Task<Guid> RegisterAsync(string title);
        Task AddUserToChatRoomAsync(Guid userId, Guid chatId);
        IAsyncEnumerable<ChatRoom> GetChatRoomListAsync(Guid userId);
    }
}
