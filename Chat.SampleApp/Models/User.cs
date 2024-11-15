namespace Chat.SampleApp.Models
{
    public sealed class User : ModelBase
    {
        public User(
            Guid id,
            string nickname,
            string fcmToken,
            IEnumerable<Guid>? chatRoomList,
            IEnumerable<Guid>? messageList) : base(id)
        {
            Nickname = nickname;
            FCMToken = fcmToken;
            ChatRoomList = chatRoomList ?? Enumerable.Empty<Guid>();
            MessageList = messageList ?? Enumerable.Empty<Guid>();
        }
        public string Nickname { get; }
        public string FCMToken { get; set; }
        public IEnumerable<Guid> ChatRoomList { get; }
        public IEnumerable<Guid> MessageList { get; }
    }
}
