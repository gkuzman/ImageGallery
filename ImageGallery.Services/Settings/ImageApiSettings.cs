using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGallery.Services.Settings
{
    public class ImageApiSettings
    {
        public string BaseInternalAddress { get; set; }

        public string BaseExternalAddress { get; set; }

        public string SeedEndpoint { get; set; }

        public string GetImageEndpoint { get; set; }
    }
}
