using System;
using System.Collections.Generic;
using System.Text;

namespace ImageGallery.Services.Requests
{
    public class VoteRequest
    {
        public string ImageId { get; set; }

        public int Mark { get; set; }
    }
}
