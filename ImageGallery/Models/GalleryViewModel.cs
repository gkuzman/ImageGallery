using ImageGallery.Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGallery.Models
{
    public class GalleryViewModel
    {
        public List<string> ImageURLS { get; set; } = new List<string>();

        public int Count { get; set; }

        public GalleryViewModel(GalleryLoadResponse galleryLoadResponse)
        {
            Count = galleryLoadResponse.Count;
            ImageURLS = galleryLoadResponse.ImageURLs;
        }
    }
}
