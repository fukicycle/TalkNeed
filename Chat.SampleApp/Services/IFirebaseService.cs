using Chat.SampleApp.Models;

namespace Chat.SampleApp.Services
{
    public interface IFirebaseService
    {
        Task<IEnumerable<T>> GetListAsync<T>(params string[] child) where T : ModelBase;
        Task<T> GetAsync<T>(params string[] child);
        Task<Guid> AddAsync<T>(T item, params string[] child) where T : ModelBase;
        Task UpdateAsync<T>(T item, params string[] child);
    }
}
