namespace Chat.SampleApp.Models
{
    public sealed class User : ModelBase
    {
        public User(
            Guid id,
            string nickname,
            IEnumerable<Guid>? chatRoomList,
            IEnumerable<Guid>? messageList) : base(id)
        {
            Nickname = nickname;
            ChatRoomList = chatRoomList ?? Enumerable.Empty<Guid>();
            MessageList = messageList ?? Enumerable.Empty<Guid>();
        }
        public string Nickname { get; }
        public IEnumerable<Guid> ChatRoomList { get; }
        public IEnumerable<Guid> MessageList { get; }
    }
}
