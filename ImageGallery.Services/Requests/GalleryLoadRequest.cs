using ImageGallery.Services.Responses;
using MediatR;

namespace ImageGallery.Services.Requests
{
    public class GalleryLoadRequest : IRequest<GalleryLoadResponse>
    {
        public GalleryLoadRequest(int pageNumber)
        {
            PageNumber = pageNumber;
        }

        public int Skip => PageNumber * Constants.Constants.NUMBER_OF_PICTURES;

        public int Take => Constants.Constants.NUMBER_OF_PICTURES;

        public int PageNumber { get; }
    }
}
