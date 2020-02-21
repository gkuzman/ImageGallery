using ImageGallery.Services.Responses;
using System.Collections.Generic;

namespace ImageGallery.Models
{
    public class SummaryViewModel
    {
        public Dictionary<string, ImageSummaryViewModel> ImageSummaries { get; set; } = new Dictionary<string, ImageSummaryViewModel>();
        public SummaryViewModel(SummaryResponse response)
        {
            foreach (var kvp in response.ImagesWithSummaries)
            {
                ImageSummaries.Add(
                    kvp.Key,
                    new ImageSummaryViewModel
                    {
                        AverageVote = kvp.Value.AverageVote.ToString("N2"),
                        TotalVotes = kvp.Value.TotalVotes,
                        UserVote = kvp.Value.UserVote
                    });
            }    
        }
    }
}
