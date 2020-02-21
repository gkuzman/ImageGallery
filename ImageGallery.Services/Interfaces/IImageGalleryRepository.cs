using ImageGallery.DAL.Entities;
using ImageGallery.Shared.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImageGallery.Services.Interfaces
{
    public interface IImageGalleryRepository : ITransientService
    {
        Task<int> AddImagesAsync(List<ImageDBO> images);

        Task<IEnumerable<ImageDBO>> GetImagesAsync(int skip, int take);

        Task<int> GetImagesCountAsync();

        Task<int> SaveUserVotesAsync(IEnumerable<UserVoteDBO> userVotes);

        Task<IEnumerable<UserVoteDBO>> GetUserVotesAsync(string userId);
    }
}
