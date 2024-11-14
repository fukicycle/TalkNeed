namespace Chat.SampleApp.Models
{
    public sealed class Message : ModelBase
    {
        public Message(
            Guid id,
            string content,
            DateTime timestamp) : base(id)
        {
            Content = content;
            Timestamp = timestamp;
        }
        public string Content { get; }
        public DateTime Timestamp { get; }
    }
}
