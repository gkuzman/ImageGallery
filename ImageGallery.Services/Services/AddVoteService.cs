using ImageGallery.Services.Interfaces;
using ImageGallery.Services.Requests;
using ImageGallery.Services.Responses;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ImageGallery.Services.Services
{
    public class AddVoteService : IRequestHandler<AddVoteRequest, AddVoteResponse>
    {
        private readonly IImageGalleryRepository _repository;
        private readonly IMapperService _mapper;

        public AddVoteService(IImageGalleryRepository repository, IMapperService mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<AddVoteResponse> Handle(AddVoteRequest request, CancellationToken cancellationToken)
        {
            var response = new AddVoteResponse();
            var votesDboList = await _mapper.MapSessionDataToUserVotesDBO();
            var votesSaved = await _repository.SaveUserVotes(votesDboList);

            if (votesSaved == 10)
            {
                response.VotingCompleted = true;
            }
            else
            {
                response.ErrorMessages.Add("Something went wront while saving user votes.");
            }

            return response;
        }
    }
}
