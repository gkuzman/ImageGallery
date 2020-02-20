using ImageGallery.API.Services.Responses;
using MediatR;

namespace ImageGallery.API.Services.Requests
{
    public class GetAllImageIdsRequest : IRequest<GetAllImageIdsResponse>
    {
    }
}
