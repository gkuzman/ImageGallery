using ImageGallery.DAL.Contexts;
using ImageGallery.DAL.Entities;
using ImageGallery.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
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
            if (await _context.Images.AnyAsync())
            {
                return;
            }

            await _context.Images.AddRangeAsync(images);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ImageDBO>> GetImages(int skip, int take)
        {
            return await _context.Images.OrderBy(x => x.Id).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<int> GetImagesCount()
        {
            return await _context.Images.CountAsync();
        }
    }
}
