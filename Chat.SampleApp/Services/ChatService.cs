using System;
using Chat.SampleApp.Models;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;

namespace Chat.SampleApp.Services;

public sealed class ChatService : IChatService
{
    private const string RESOURCE_NAME = "messages_1";
    private readonly IFirebaseService _firebaseService;
    private readonly HttpClient _httpClient;
    public ChatService(
        IFirebaseService firebaseService,
        HttpClient httpClient)
    {
        _firebaseService = firebaseService;
        _httpClient = httpClient;
    }

    public async Task<Message> AddNewMessage(Guid chatId, Guid userId, string message)
    {
        User user = await _firebaseService.GetAsync<User>("users_1", userId.ToString());
        Message newMessage = new Message(Guid.NewGuid(), user.Nickname, message, DateTime.Now);
        Task task = Task.Run(async () =>
        {
            ChatRoom chatRoom = await _firebaseService.GetAsync<ChatRoom>("chat_rooms_1", chatId.ToString());
            Guid id = await _firebaseService.AddAsync(newMessage, RESOURCE_NAME, chatId.ToString());
            IEnumerable<Guid> chatRoomMessageIdList = chatRoom.MessageList.Append(id);
            IEnumerable<Guid> userMessageIdList = user.MessageList.Append(id);
            await _firebaseService.UpdateAsync(new ChatRoom(chatRoom.Id, chatRoom.Title, message, newMessage.Timestamp, chatRoomMessageIdList), "chat_rooms_1", chatId.ToString());
            await _firebaseService.UpdateAsync(new User(userId, user.Nickname, user.ChatRoomList, userMessageIdList), "users_1", userId.ToString());
        });
        return newMessage;
    }

    public async Task<List<Message>> GetMessageListAsync(Guid chatId)
    {
        return (await _firebaseService.GetListAsync<Message>(RESOURCE_NAME, chatId.ToString())).OrderBy(a => a.Timestamp).ToList();
    }

    public async Task ListenAsync(Guid chatId, Action<Message> onNext, Action<Exception> onError)
    {
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{RESOURCE_NAME}/{chatId}/.json");
        requestMessage.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/event-stream"));
        requestMessage.SetBrowserResponseStreamingEnabled(true);
        HttpResponseMessage responseMessage = await _httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
        Task task = Task.Run(async () =>
        {
            try
            {
                using Stream stream = await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);
                using StreamReader reader = new StreamReader(stream);
                string eventName = string.Empty;
                while (true)
                {
                    try
                    {
                        string? line = await reader.ReadLineAsync();
                        if (string.IsNullOrEmpty(line))
                        {
                            continue;
                        }
                        if (line.StartsWith("event: "))
                        {
                            eventName = line[7..10];
                            continue;
                        }
                        if (line.StartsWith("data: ") && eventName == "put")
                        {
                            int firstIndex = line.IndexOf(",");
                            int startIndex = line.IndexOf("path");
                            string tmpRefPath = line.Substring(startIndex - 1, firstIndex - startIndex);
                            string refPath = tmpRefPath.Split(':')[1][1..];
                            Message message = await _firebaseService.GetAsync<Message>("messages_1", chatId.ToString(), refPath).ConfigureAwait(false);
                            if (message.Timestamp != DateTime.MinValue)
                            {
                                onNext(message);
                            }
                            eventName = string.Empty;
                        }
                    }
                    catch (Exception ex)
                    {
                        onError(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                onError(ex);
            }
        });
    }
}
