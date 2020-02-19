using ImageGallery.DAL.Contexts;
using ImageGallery.DAL.Entities;
using ImageGallery.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageGallery.Services.Services
{
    public class ImageGalleryRepository : IImageGalleryRepository
    {
        private readonly ImageGalleryContext _context;

        public ImageGalleryRepository(ImageGalleryContext context)
        {
            _context = context;
        }
        public async  Task AddImages(List<ImageDBO> images)
        {
            if (_context.Images.Any())
            {
                return;
            }

            await _context.Images.AddRangeAsync(images);
            await _context.SaveChangesAsync();
        }
    }
}
