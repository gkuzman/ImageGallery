using System.Threading.Tasks;

namespace ImageGallery.Services.Interfaces
{
    public interface IDatabasesSyncService
    {
        Task SyncImageGallery(int retryCount = 0);
    }
}
