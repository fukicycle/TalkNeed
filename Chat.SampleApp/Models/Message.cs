namespace Chat.SampleApp.Models
{
    public sealed class Message : ModelBase
    {
        public Message(
            Guid id,
            string sender,
            string content,
            DateTime timestamp) : base(id)
        {
            Sender = sender;
            Content = content;
            Timestamp = timestamp;
        }
        public string Sender { get; }
        public string Content { get; }
        public DateTime Timestamp { get; }
    }
}
