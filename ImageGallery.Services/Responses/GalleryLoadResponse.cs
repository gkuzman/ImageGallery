using ImageGallery.Shared.Response;
using System.Collections.Generic;

namespace ImageGallery.Services.Responses
{
    public class GalleryLoadResponse : ResponseBase
    {
        public List<string> ImageURLs { get; set; } = new List<string>();

        public int Count { get; set; }
    }
}
