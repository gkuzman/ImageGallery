using ImageGallery.Shared.Response;
using System.Collections.Generic;

namespace ImageGallery.API.Services.Responses
{
    public class GetAllImageIdsResponse : ResponseBase
    {
        public IEnumerable<string> ImageIds { get; set; } = new List<string>();
    }
}
