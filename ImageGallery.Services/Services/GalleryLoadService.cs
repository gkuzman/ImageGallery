using ImageGallery.Services.Interfaces;
using ImageGallery.Services.Requests;
using ImageGallery.Services.Responses;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ImageGallery.Services.Services
{
    public class GalleryLoadService : IRequestHandler<GalleryLoadRequest, GalleryLoadResponse>
    {
        private readonly IImageGalleryRepository _repository;
        private readonly IMapperService _mapperService;
        private readonly IDistributedCache _cache;

        public GalleryLoadService(IImageGalleryRepository repository, IMapperService mapperService, IDistributedCache cache)
        {
            _repository = repository;
            _mapperService = mapperService;
            _cache = cache;
        }
        public async Task<GalleryLoadResponse> Handle(GalleryLoadRequest request, CancellationToken cancellationToken)
        {
            var fromCache = await _cache.GetStringAsync(request.PageNumber.ToString());
            var imageIdList = new List<string>();
            var totalNumberOfImages = 0;

            if (string.IsNullOrEmpty(fromCache))
            {
                var images = await _repository.GetImagesAsync(request.Skip, request.Take);
                totalNumberOfImages = await _repository.GetImagesCountAsync();

                if (images.Any())
                {
                    foreach (var image in images)
                    {
                        imageIdList.Add(image.Id);
                    }
                    await _cache.SetStringAsync(request.PageNumber.ToString(), JsonConvert.SerializeObject(imageIdList));
                    await _cache.SetStringAsync(Constants.Constants.TOTAL, totalNumberOfImages.ToString());
                }
                else
                {
                    var response = new GalleryLoadResponse();
                    response.ErrorMessages.Add("Something went wront while trying to retrieve images from the database");
                    return response;
                }
            }
            else
            {
                imageIdList = JsonConvert.DeserializeObject<List<string>>(fromCache);
                totalNumberOfImages = int.Parse(await _cache.GetStringAsync(Constants.Constants.TOTAL));
            }

            return await _mapperService.CreateGalleryLoadResponseAsync(request, imageIdList, totalNumberOfImages);
        }
    }
}
