using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGallery.Services.Settings
{
    public class ImageApiSettings
    {
        public string BaseAddress { get; set; }

        public string SeedEndpoint { get; set; }

        public string GetImageEndpoint { get; set; }
    }
}
