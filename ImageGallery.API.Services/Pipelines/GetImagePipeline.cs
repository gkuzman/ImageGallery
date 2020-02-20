using ImageGallery.API.Services.Requests;
using ImageGallery.API.Services.Responses;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Threading;
using System.Threading.Tasks;

namespace ImageGallery.API.Services.Pipelines
{
    public class GetImagePipeline : IPipelineBehavior<GetImageRequest, GetImageResponse>
    {
        private readonly IDistributedCache _cache;

        public GetImagePipeline(IDistributedCache cache)
        {
            _cache = cache;
        }
        public async Task<GetImageResponse> Handle(GetImageRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<GetImageResponse> next)
        {
            var fromCache = await _cache.GetAsync(request.ImageId);

            if (fromCache is null)
            {
                var result = await next();

                if (!(result.Content is null))
                {
                    await _cache.SetAsync(result.ImageId, result.Content);
                }

                return result;
            }

            return new GetImageResponse { ImageId = request.ImageId, Content = fromCache };
        }
    }
}
