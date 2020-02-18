using System.Collections.Generic;

namespace ImageGallery.Shared.Response
{
    public abstract class ResponseBase
    {
        public List<string> ErrorMessages { get; set; } = new List<string>();
        public bool HasErrors => ErrorMessages.Count > 0;
    }
}
