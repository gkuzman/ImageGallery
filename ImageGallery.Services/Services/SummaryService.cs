using ImageGallery.Services.Interfaces;
using ImageGallery.Services.Requests;
using ImageGallery.Services.Responses;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ImageGallery.Services.Services
{
    public class SummaryService : IRequestHandler<SummaryRequest, SummaryResponse>
    {
        private readonly IImageGalleryRepository _repository;
        private readonly IMapperService _mapper;

        public SummaryService(IImageGalleryRepository repository, IMapperService mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<SummaryResponse> Handle(SummaryRequest request, CancellationToken cancellationToken)
        {
            var response = new SummaryResponse();
            var votes = await _repository.GetUserVotes(request.UserId);

            if (votes.Count() != 10)
            {
                response.ErrorMessages.Add("Something went wrong while trying to fetch the user votes");
                response.ShowSummary = false;
                return response;
            }
            
            return _mapper.MapVotesToSummaryResponse(votes);
        }
    }
}
