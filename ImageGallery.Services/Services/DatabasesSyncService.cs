using ImageGallery.Services.Interfaces;
using ImageGallery.Services.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ImageGallery.Services.Services
{
    public class DatabasesSyncService : IDatabasesSyncService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<DatabasesSyncService> _logger;
        private readonly IMapperService _mapperService;
        private readonly IImageGalleryRepository _repository;
        private readonly IOptions<ImageApiSettings> _settings;

        public DatabasesSyncService(HttpClient httpClient,
            ILogger<DatabasesSyncService> logger,
            IMapperService mapperService,
            IImageGalleryRepository repository,
            IOptions<ImageApiSettings> settings)
        {
            _httpClient = httpClient;
            _logger = logger;
            _mapperService = mapperService;
            _repository = repository;
            _settings = settings;
        }
        public async Task SyncImageGallery(int retryCount = 0)
        {
            try
            {
                if (await _repository.GetImagesCount() > 0)
                {
                    return;
                }
                if (retryCount > 3)
                {
                    throw new Exception("Couldnt import image ids from API");
                }
                var httpRequest = await _httpClient.GetAsync(_settings.Value.SeedEndpoint);

                if (httpRequest.IsSuccessStatusCode)
                {
                    var content = await httpRequest.Content.ReadAsStringAsync();
                    var toInsert = _mapperService.MapApiResponseToImageEntities(content);
                    if (await _repository.AddImages(toInsert) != toInsert.Count) 
                    {
                        throw new Exception("Not all images could be imported.");
                    };
                }
                else
                {
                    retryCount++;
                    await Task.Delay(5000);
                    await SyncImageGallery(retryCount);
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "An exception occured while trying to import image ids from API");
                throw;
            }
        }
    }
}
