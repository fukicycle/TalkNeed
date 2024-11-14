
using Chat.SampleApp.Models;
using Firebase.Database;
using Firebase.Database.Query;

namespace Chat.SampleApp.Services
{
    public sealed class FirebaseService : IFirebaseService
    {
        private readonly FirebaseClient _client = new FirebaseClient("https://test-chat-36e47-default-rtdb.firebaseio.com");
        public async Task<Guid> AddAsync<T>(T item, params string[] child) where T : ModelBase
        {
            ChildQuery query = Prepare(child);
            await query.Child(item.Id.ToString()).PutAsync(item);
            return item.Id;
        }

        public async Task<T> GetAsync<T>(params string[] child)
        {
            ChildQuery query = Prepare(child);
            var response = await query.OnceSingleAsync<T>();
            return response;
        }

        public async Task<IEnumerable<T>> GetListAsync<T>(params string[] child) where T : ModelBase
        {
            ChildQuery query = Prepare(child);
            var response = await query.OnceAsync<T>();
            return response.Select(a => a.Object);
        }

        public async Task UpdateAsync<T>(T item, params string[] child)
        {
            ChildQuery query = Prepare(child);
            await query.PutAsync(item);
        }

        private ChildQuery Prepare(params string[] child)
        {
            if (child == null || child.Length == 0)
            {
                throw new ArgumentNullException(nameof(child));
            }
            ChildQuery query = _client.Child(child[0]);
            for (int i = 1; i < child.Length; i++)
            {
                query = query.Child(child[i]);
            }
            return query;
        }
    }
}
