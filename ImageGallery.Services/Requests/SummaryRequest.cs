using ImageGallery.Services.Responses;
using MediatR;

namespace ImageGallery.Services.Requests
{
    public class SummaryRequest : IRequest<SummaryResponse>
    {
        public string UserId { get; set; }
    }
}
