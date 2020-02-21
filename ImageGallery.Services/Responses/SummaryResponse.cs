using ImageGallery.Shared.Response;
using System.Collections.Generic;

namespace ImageGallery.Services.Responses
{
    public class SummaryResponse : ResponseBase
    {
        public bool ShowSummary { get; set; } = true;

        public Dictionary<string, ImageSummary> ImagesWithSummaries { get; set; } = new Dictionary<string, ImageSummary>();
    }

    public class ImageSummary
    {
        public int UserVote { get; set; }

        public int TotalVotes { get; set; }

        public decimal AverageVote { get; set; }
    }
}

