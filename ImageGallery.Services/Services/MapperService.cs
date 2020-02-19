using ImageGallery.DAL.Entities;
using ImageGallery.Services.Interfaces;
using ImageGallery.Services.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageGallery.Services.Services
{
    public class MapperService : IMapperService
    {
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
                response.ImageIds.Add(image.Id);
            }

            response.Count = totalCount;

            return response;
        }
    }
}
