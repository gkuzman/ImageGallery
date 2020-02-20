using ImageGallery.Services.Interfaces;
using ImageGallery.Services.Requests;
using ImageGallery.Services.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
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
            var votesSoFar = await _session.ReadFromSessionString<Dictionary<string, int>>("votes");
            var response = new AddVoteResponse();
            if (votesSoFar.Count > 10)
            {
                response.ErrorMessages.Add("You cannot have more than 10 votes");
                return response;
            }
            else
            {
                if (votesSoFar.ContainsKey(request.ImageId))
                {
                    votesSoFar[request.ImageId] = request.Mark;
                }
                else
                {
                    votesSoFar.Add(request.ImageId, request.Mark);
                }

                await _session.SetObjectToStringSession("votes", votesSoFar);

                if (votesSoFar.Count == 10)
                {
                    response = await next();
                }

                response.VotesLeft = 10 - votesSoFar.Count;

                return response;
            }
        }
    }
}
