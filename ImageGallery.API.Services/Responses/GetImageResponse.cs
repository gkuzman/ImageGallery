using ImageGallery.Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageGallery.API.Services.Responses
{
    public class GetImageResponse : ResponseBase
    {
        public string ImageId { get; set; }

        public byte[] Content { get; set; }
    }
}
