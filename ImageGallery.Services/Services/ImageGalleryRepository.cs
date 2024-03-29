﻿using ImageGallery.DAL.Contexts;
using ImageGallery.DAL.Entities;
using ImageGallery.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<int> AddImagesAsync(List<ImageDBO> images)
        {
            await _context.Images.AddRangeAsync(images);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ImageDBO>> GetImagesAsync(int skip, int take)
        {
            return await _context.Images.OrderBy(x => x.Id).Skip(skip).Take(take).AsNoTracking().ToListAsync();
        }

        public async Task<int> GetImagesCountAsync()
        {
            return await _context.Images.AsNoTracking().CountAsync();
        }

        public async Task<int> SaveUserVotesAsync(IEnumerable<UserVoteDBO> userVotes)
        {
            await _context.UserVotes.AddRangeAsync(userVotes);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserVoteDBO>> GetUserVotesAsync(string userId)
        {
            return await _context.UserVotes.Where(x => x.UserId == userId)
                                           .Include(x => x.Image).ThenInclude(y => y.UserVotes).ToListAsync();
        }
    }
}
