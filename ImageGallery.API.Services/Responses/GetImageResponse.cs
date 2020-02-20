using ImageGallery.Shared.Response;

namespace ImageGallery.API.Services.Responses
{
    public class GetImageResponse : ResponseBase
    {
        public string ImageId { get; set; }

        public byte[] Content { get; set; }
    }
}
