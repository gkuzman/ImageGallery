using ImageGallery.Services.Responses;
using MediatR;

namespace ImageGallery.Services.Requests
{
    public class GalleryLoadRequest : IRequest<GalleryLoadResponse>
    {
        private int _pageNumber;
        private const int NUMBER_OF_PICTURES = 15;

        public GalleryLoadRequest(int pageNumber)
        {
            _pageNumber = pageNumber;
        }

        public int Skip => _pageNumber * NUMBER_OF_PICTURES;

        public int Take => NUMBER_OF_PICTURES;
    }
}
