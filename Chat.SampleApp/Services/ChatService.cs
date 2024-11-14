using System;
using Chat.SampleApp.Models;

namespace Chat.SampleApp.Services;

public sealed class ChatService : IChatService
{
    private const string RESOURCE_NAME = "messages_1";
    private readonly IFirebaseService _firebaseService;
    public ChatService(IFirebaseService firebaseService)
    {
        _firebaseService = firebaseService;
    }

    public async Task AddNewMessage(Guid chatId, Guid userId, string message)
    {
        ChatRoom chatRoom = await _firebaseService.GetAsync<ChatRoom>("chat_rooms_1", chatId.ToString());
        User user = await _firebaseService.GetAsync<User>("users_1", userId.ToString());
        Message newMessage = new Message(Guid.NewGuid(), user.Nickname, message, DateTime.Now);
        Guid id = await _firebaseService.AddAsync(newMessage, RESOURCE_NAME, chatId.ToString());
        IEnumerable<Guid> chatRoomMessageIdList = chatRoom.MessageList.Append(id);
        IEnumerable<Guid> userMessageIdList = user.MessageList.Append(id);
        await _firebaseService.UpdateAsync(new ChatRoom(chatRoom.Id, chatRoom.Title, message, newMessage.Timestamp, chatRoomMessageIdList), "chat_rooms_1", chatId.ToString());
        await _firebaseService.UpdateAsync(new User(userId, user.Nickname, user.ChatRoomList, userMessageIdList), "users_1", userId.ToString());
    }

    public async Task<IEnumerable<Message>> GetMessageListAsync(Guid chatId)
    {
        return await _firebaseService.GetListAsync<Message>(RESOURCE_NAME, chatId.ToString());
    }
}
