using ImageGallery.API.Services.Responses;
using MediatR;

namespace ImageGallery.API.Services.Requests
{
    public class GetImageRequest : IRequest<GetImageResponse>
    {
        public string ImageId { get; set; }
    }
}
