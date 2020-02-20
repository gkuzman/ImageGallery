using ImageGallery.DAL.Entities;
using ImageGallery.Services.Interfaces;
using ImageGallery.Services.Responses;
using ImageGallery.Services.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageGallery.Services.Services
{
    public class MapperService : IMapperService
    {
        private readonly IOptions<ImageApiSettings> _settings;

        public MapperService(IOptions<ImageApiSettings> settings)
        {
            _settings = settings;
        }
        public List<ImageDBO> MapApiResponseToImageEntities(string response)
        {
            var listOfIds = JsonConvert.DeserializeObject<List<string>>(response);
            var result = new List<ImageDBO>();

            foreach (var id in listOfIds)
            {
                result.Add(new ImageDBO { Id = id });
            }

            return result;
        }

        public GalleryLoadResponse MapDBOToGalleryLoadResponse(IEnumerable<ImageDBO> images, int totalCount)
        {
            var response = new GalleryLoadResponse();
            foreach (var image in images)
            {
                response.ImageURLs.Add($"{_settings.Value.BaseExternalAddress}{_settings.Value.GetImageEndpoint}{image.Id}");
            }

            response.Count = totalCount;

            return response;
        }
    }
}
