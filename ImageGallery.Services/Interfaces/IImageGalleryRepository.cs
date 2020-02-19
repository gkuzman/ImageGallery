using ImageGallery.DAL.Entities;
using ImageGallery.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImageGallery.Services.Interfaces
{
    public interface IImageGalleryRepository : ITransientService
    {
        Task AddImages(List<ImageDBO> images);

        Task<IEnumerable<ImageDBO>> GetImages(int skip, int take);

        Task<int> GetImagesCount();
    }
}
