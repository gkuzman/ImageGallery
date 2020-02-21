using ImageGallery.Shared.Interfaces;
using System.Threading.Tasks;

namespace ImageGallery.Services.Interfaces
{
    public interface ISessionAccessor : ITransientService
    {
        Task InitializeSessionAsync();

        Task<T> ReadFromSessionStringAsync<T>(string key) where T : new();

        Task SetObjectToStringSessionAsync<T>(string key, T value) where T : new();

        Task<string> GetSessionIdAsync();
    }
}
