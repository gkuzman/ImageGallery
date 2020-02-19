using ImageGallery.Services.Interfaces;
using ImageGallery.Services.Requests;
using ImageGallery.Services.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ImageGallery.Services.Services
{
    public class GalleryLoadService : IRequestHandler<GalleryLoadRequest, GalleryLoadResponse>
    {
        private readonly IImageGalleryRepository _repository;
        private readonly IMapperService _mapperService;

        public GalleryLoadService(IImageGalleryRepository repository, IMapperService mapperService)
        {
            _repository = repository;
            _mapperService = mapperService;
        }
        public async Task<GalleryLoadResponse> Handle(GalleryLoadRequest request, CancellationToken cancellationToken)
        {
            var images = await _repository.GetImages(request.Skip, request.Take);
            var totalNumberOfImages = await _repository.GetImagesCount();

            return _mapperService.MapDBOToGalleryLoadResponse(images, totalNumberOfImages);
        }
    }
}
