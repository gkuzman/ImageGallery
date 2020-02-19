using ImageGallery.Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageGallery.API.Services.Responses
{
    public class GetAllImageIdsResponse : ResponseBase
    {
        public List<string> ImageIds { get; set; } = new List<string>();
    }
}
