using ImageGallery.DAL.Entities;
using ImageGallery.Services.Requests;
using ImageGallery.Services.Responses;
using ImageGallery.Shared.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImageGallery.Services.Interfaces
{
    public interface IMapperService : ITransientService
    {
        List<ImageDBO> MapApiResponseToImageEntities(string response);

        Task<GalleryLoadResponse> CreateGalleryLoadResponse(GalleryLoadRequest request, IEnumerable<string> imageIds, int totalCount);

        Task<IEnumerable<UserVoteDBO>> MapSessionDataToUserVotesDBO();
    }
}
