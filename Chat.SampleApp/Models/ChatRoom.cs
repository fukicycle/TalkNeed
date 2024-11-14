namespace Chat.SampleApp.Models
{
    public sealed class ChatRoom : ModelBase
    {
        public ChatRoom(
            Guid id,
            string title,
            string lastMessage,
            DateTime timestamp,
            IEnumerable<Guid>? messageList) : base(id)
        {
            Title = title;
            LastMessage = lastMessage;
            Timestamp = timestamp;
            MessageList = messageList ?? Enumerable.Empty<Guid>();
        }
        public string Title { get; }
        public string LastMessage { get; }
        public DateTime Timestamp { get; }
        public IEnumerable<Guid> MessageList { get; }
    }
}
