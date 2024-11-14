using System;
using Chat.SampleApp.Models;

namespace Chat.SampleApp.Services;

public interface IChatService
{
    Task<IEnumerable<Message>> GetMessageListAsync(Guid chatId);
    Task AddNewMessage(Guid chatId, Guid userId, string message);
}
