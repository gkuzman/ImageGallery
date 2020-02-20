using ImageGallery.Services.Responses;
using MediatR;

namespace ImageGallery.Services.Requests
{
    public class AddVoteRequest : IRequest<AddVoteResponse>
    {
        public string ImageId { get; set; }

        public int Mark { get; set; }
    }
}
