using ImageGallery.Services.Interfaces;
using ImageGallery.Services.Requests;
using ImageGallery.Services.Responses;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ImageGallery.Services.Pipelines
{
    public class GalleryLoadPipeline : IPipelineBehavior<GalleryLoadRequest, GalleryLoadResponse>
    {
        private readonly ISessionAccessor _sessionAccessor;
        private readonly IDistributedCache _cache;

        public GalleryLoadPipeline(ISessionAccessor sessionAccessor, IDistributedCache cache)
        {
            _sessionAccessor = sessionAccessor;
            _cache = cache;
        }
        public async Task<GalleryLoadResponse> Handle(GalleryLoadRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<GalleryLoadResponse> next)
        {
            await _sessionAccessor.InitializeSession();
            var fromCache = await _cache.GetStringAsync(request.PageNumber.ToString());

            if (string.IsNullOrEmpty(fromCache))
            {
                var result = await next();

                if (!result.ErrorMessages.Any())
                {
                    await _cache.SetStringAsync(request.PageNumber.ToString(), JsonConvert.SerializeObject(result));
                }
                
                return result;
            }

            return JsonConvert.DeserializeObject<GalleryLoadResponse>(fromCache);
        }
    }
}
