using System.Threading.Tasks;

namespace ImageGallery.Services.Interfaces
{
    public interface IDatabasesSyncService
    {
        Task SyncImageGalleryAsync(int retryCount = 0);
    }
}
