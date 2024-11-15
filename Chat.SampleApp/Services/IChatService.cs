using System;
using Chat.SampleApp.Models;

namespace Chat.SampleApp.Services;

public interface IChatService
{
    Task<List<Message>> GetMessageListAsync(Guid chatId);
    Task<Message> AddNewMessage(Guid chatId, Guid userId, string message);
    Task ListenAsync(Guid chatId, Action<Message> onNext, Action<Exception> onError);
}
