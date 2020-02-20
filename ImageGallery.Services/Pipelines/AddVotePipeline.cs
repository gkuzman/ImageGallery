using ImageGallery.Services.Interfaces;
using ImageGallery.Services.Requests;
using ImageGallery.Services.Responses;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ImageGallery.Services.Pipelines
{
    public class AddVotePipeline : IPipelineBehavior<AddVoteRequest, AddVoteResponse>
    {
        private readonly ISessionAccessor _session;

        public AddVotePipeline(ISessionAccessor session)
        {
            _session = session;
        }
        public async Task<AddVoteResponse> Handle(AddVoteRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<AddVoteResponse> next)
        {
            var response = new AddVoteResponse();

            if (request.Mark > 10 || request.Mark < 0)
            {
                response.ErrorMessages.Add("Invalid mark");
                return response;
            }

            var votesSoFar = await _session.ReadFromSessionString<Dictionary<string, int>>("votes");
            
            if (votesSoFar.Count >= 10)
            {
                response.ErrorMessages.Add("You cannot have more than 10 votes");
                return response;
            }
            else
            {
                if (request.Mark > 0)
                {
                    AddVote(request, votesSoFar);
                }
                else
                {
                    RemoveVote(request, votesSoFar);
                }

                await _session.SetObjectToStringSession("votes", votesSoFar);

                if (votesSoFar.Count == 10)
                {
                    response = await next();
                    await _session.SetObjectToStringSession("votingDone", response.VotingCompleted);
                }

                response.VotesLeft = 10 - votesSoFar.Count;

                return response;
            }
        }

        private void AddVote(AddVoteRequest request, Dictionary<string, int> votesSoFar)
        {
            if (votesSoFar.ContainsKey(request.ImageId))
            {
                votesSoFar[request.ImageId] = request.Mark;
            }
            else
            {
                votesSoFar.Add(request.ImageId, request.Mark);
            }
        }

        private void RemoveVote(AddVoteRequest request, Dictionary<string, int> votesSoFar)
        {
            if (votesSoFar.ContainsKey(request.ImageId))
            {
                votesSoFar.Remove(request.ImageId);
            }
        }
    }
}
