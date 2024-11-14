using Chat.SampleApp.Models;

namespace Chat.SampleApp.Services
{
    public sealed class UserService : IUserService
    {
        private const string RESOURCE_NAME = "users_1";
        private readonly IFirebaseService _firebaseService;
        public UserService(IFirebaseService firebaseService)
        {
            _firebaseService = firebaseService;
        }
        public async Task<User> GetUserAsync(Guid id)
        {
            return await _firebaseService.GetAsync<User>(RESOURCE_NAME, id.ToString());
        }

        public async Task<Guid> RegisterAsync(string nickname)
        {
            if (string.IsNullOrEmpty(nickname))
            {
                throw new ArgumentNullException(nameof(nickname));
            }
            User user = new User(Guid.NewGuid(), nickname, Enumerable.Empty<Guid>());
            return await _firebaseService.AddAsync(user, RESOURCE_NAME);
        }
    }
}
