using Chat.SampleApp.Models;

namespace Chat.SampleApp.Services
{
    public interface IUserService
    {
        Task<Guid> RegisterAsync(string nickname);
        Task<User> GetUserAsync(Guid id);
        Task AppendFCMTokenAsync(Guid id, string fcmToken);
    }
}
