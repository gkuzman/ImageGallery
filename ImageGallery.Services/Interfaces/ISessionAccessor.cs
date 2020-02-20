using ImageGallery.Shared.Interfaces;
using System.Threading.Tasks;

namespace ImageGallery.Services.Interfaces
{
    public interface ISessionAccessor : ITransientService
    {
        Task InitializeSession();

        Task<T> ReadFromSessionString<T>(string key) where T : new();

        Task SetObjectToStringSession<T>(string key, T value) where T : new();
    }
}
