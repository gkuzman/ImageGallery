using ImageGallery.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageGallery.DAL.Contexts
{
    public class ImageGalleryContext : DbContext
    {
        public ImageGalleryContext(DbContextOptions<ImageGalleryContext> options) : base(options)
        {

        }

        public virtual DbSet<ImageDBO> Images { get; set; }
        public virtual DbSet<UserVoteDBO> UserVotes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ImageDBO>().HasKey(x => x.Id);
            builder.Entity<UserVoteDBO>().HasKey(x => new { x.ImageId, x.UserId });
            builder.Entity<UserVoteDBO>().HasOne(x => x.Image).WithMany(y => y.UserVotes).IsRequired();
        }
    }
}
