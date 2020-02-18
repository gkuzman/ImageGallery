using ImageGallery.Shared.Interfaces;
using System.Threading.Tasks;

namespace ImageGallery.Services.Interfaces
{
    public interface ISessionAccessor : ITransientService
    {
        Task InitializeSession();
    }
}
