using ImageGallery.API.DAL.Services;
using ImageGallery.API.Services.Requests;
using ImageGallery.API.Services.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ImageGallery.API.Services.Services
{
    public class GetAllImageIdsService : IRequestHandler<GetAllImageIdsRequest, GetAllImageIdsResponse>
    {
        private readonly ImageService _imageService;

        public GetAllImageIdsService(ImageService imageService)
        {
            _imageService = imageService;
        }
        public async Task<GetAllImageIdsResponse> Handle(GetAllImageIdsRequest request, CancellationToken cancellationToken)
        {
            var response = new GetAllImageIdsResponse();
            response.ImageIds = await _imageService.GetAllIdsAsync();
            return response;
        }
    }
}
