using ImageGallery.Services.Requests;
using ImageGallery.Services.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ImageGallery.Services.Services
{
    public class GalleryLoadService : IRequestHandler<GalleryLoadRequest, GalleryLoadResponse>
    {
        public async Task<GalleryLoadResponse> Handle(GalleryLoadRequest request, CancellationToken cancellationToken)
        {
            await Task.Delay(1);
            return new GalleryLoadResponse();
        }
    }
}
