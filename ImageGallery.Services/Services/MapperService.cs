using ImageGallery.DAL.Entities;
using ImageGallery.Services.Interfaces;
using ImageGallery.Services.Requests;
using ImageGallery.Services.Responses;
using ImageGallery.Services.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
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

        public async Task<GalleryLoadResponse> CreateGalleryLoadResponse(GalleryLoadRequest request, IEnumerable<string> imageIds, int totalCount)
        {
            var response = new GalleryLoadResponse();
            var castedVotes = await _session.ReadFromSessionString<Dictionary<string, int>>("votes");
            foreach (var imageId in imageIds)
            {
                castedVotes.TryGetValue(imageId, out var vote);
                response.ImageURLsAndVotes.Add($"{_settings.Value.BaseExternalAddress}{_settings.Value.GetImageEndpoint}{imageId}", vote);
            }

            response.VotesRemaining = 10 - castedVotes.Count;
            response.Count = totalCount;
            response.CurrentPage = request.PageNumber;

            return response;
        }

        public async Task<IEnumerable<UserVoteDBO>> MapSessionDataToUserVotesDBO()
        {
            var votes = await _session.ReadFromSessionString<Dictionary<string, int>>("votes");

            if (votes.Count != 10)
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
    }
}
