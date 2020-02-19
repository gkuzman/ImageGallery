using ImageGallery.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ImageGallery.Services.Services
{
    public class DatabasesSyncService : IDatabasesSyncService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<DatabasesSyncService> _logger;
        private readonly IMapperService _mapperService;
        private readonly IImageGalleryRepository _repository;

        public DatabasesSyncService(HttpClient httpClient, ILogger<DatabasesSyncService> logger, IMapperService mapperService, IImageGalleryRepository repository)
        {
            _httpClient = httpClient;
            _logger = logger;
            _mapperService = mapperService;
            _repository = repository;
        }
        public async Task SyncImageGallery(int retryCount = 0)
        {
            try
            {
                if (retryCount > 3)
                {
                    throw new Exception("Couldnt import image ids from API");
                }
                var httpRequest = await _httpClient.GetAsync("/api/images/allids");

                if (httpRequest.IsSuccessStatusCode)
                {
                    var content = await httpRequest.Content.ReadAsStringAsync();
                    var toInsert = _mapperService.MapApiResponseToImageEntities(content);
                    await _repository.AddImages(toInsert);
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
