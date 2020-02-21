using ImageGallery.DAL.Entities;
using ImageGallery.Services.Interfaces;
using ImageGallery.Services.Requests;
using ImageGallery.Services.Responses;
using ImageGallery.Services.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGallery.Services.Services
{
    public class MapperService : IMapperService
    {
        private readonly IOptions<ImageApiSettings> _settings;
        private readonly ISessionAccessor _session;

        public MapperService(IOptions<ImageApiSettings> settings, ISessionAccessor session)
        {
            _settings = settings;
            _session = session;
        }
        public List<ImageDBO> MapApiResponseToImageEntities(string response)
        {
            var listOfIds = JsonConvert.DeserializeObject<List<string>>(response);
            var result = new List<ImageDBO>();

            foreach (var id in listOfIds)
            {
                result.Add(new ImageDBO { Id = id });
            }

            return result;
        }

        public async Task<GalleryLoadResponse> CreateGalleryLoadResponseAsync(GalleryLoadRequest request, IEnumerable<string> imageIds, int totalCount)
        {
            var response = new GalleryLoadResponse();
            var castedVotes = await _session.ReadFromSessionStringAsync<Dictionary<string, int>>(Constants.Constants.USER_VOTES);
            foreach (var imageId in imageIds)
            {
                castedVotes.TryGetValue(imageId, out var vote);
                response.ImageURLsAndVotes.Add($"{_settings.Value.BaseExternalAddress}{_settings.Value.GetImageEndpoint}{imageId}", vote);
            }

            response.VotesRemaining = Constants.Constants.NUMBER_OF_VOTES - castedVotes.Count;
            response.Count = totalCount;
            response.CurrentPage = request.PageNumber;

            return response;
        }

        public async Task<IEnumerable<UserVoteDBO>> MapSessionDataToUserVotesDBOAsync()
        {
            var votes = await _session.ReadFromSessionStringAsync<Dictionary<string, int>>(Constants.Constants.USER_VOTES);

            if (votes.Count != Constants.Constants.NUMBER_OF_VOTES)
            {
                throw new System.Exception("Something went wrong! Cannot save the user votes");
            }

            var result = new List<UserVoteDBO>();

            var sessionId = await _session.GetSessionIdAsync();

            foreach (var vote in votes)
            {
                result.Add(new UserVoteDBO { ImageId = vote.Key, Mark = vote.Value, UserId = sessionId });
            }

            return result;
        }

        public SummaryResponse MapVotesToSummaryResponse(IEnumerable<UserVoteDBO> votes)
        {
            var response = new SummaryResponse();
            foreach (var vote in votes)
            {
                response.ImagesWithSummaries.Add($"{_settings.Value.BaseExternalAddress}{_settings.Value.GetImageEndpoint}{vote.ImageId}", GetImageSummary(vote));
            }

            return response;
        }

        private ImageSummary GetImageSummary(UserVoteDBO vote)
        {
            var summary = new ImageSummary();
            summary.UserVote = vote.Mark;
            summary.TotalVotes = vote.Image.UserVotes.Count();
            summary.AverageVote = decimal.Divide(vote.Image.UserVotes.Sum(x => x.Mark), summary.TotalVotes);

            return summary;
        }
    }
}
