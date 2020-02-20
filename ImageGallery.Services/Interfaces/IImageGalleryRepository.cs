using ImageGallery.DAL.Entities;
using ImageGallery.Shared.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImageGallery.Services.Interfaces
{
    public interface IImageGalleryRepository : ITransientService
    {
        Task<int> AddImages(List<ImageDBO> images);

        Task<IEnumerable<ImageDBO>> GetImages(int skip, int take);

        Task<int> GetImagesCount();

        Task<int> SaveUserVotes(IEnumerable<UserVoteDBO> userVotes);
    }
}
