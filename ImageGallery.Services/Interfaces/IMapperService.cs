using ImageGallery.DAL.Entities;
using ImageGallery.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageGallery.Services.Interfaces
{
    public interface IMapperService : ITransientService
    {
        List<ImageDBO> MapApiResponseToImageEntities(string response);
    }
}
