using ImageGallery.Shared.Response;
using System.Collections.Generic;

namespace ImageGallery.Services.Responses
{
    public class GalleryLoadResponse : ResponseBase
    {
        public Dictionary<string, int> ImageURLsAndVotes { get; set; } = new Dictionary<string, int>();

        public int Count { get; set; }

        public int CurrentPage { get; set; }
    }
}
