using ImageGallery.Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageGallery.Services.Responses
{
    public class SummaryResponse : ResponseBase
    {
        public bool ShowSummary { get; set; } = true;
    }
}
