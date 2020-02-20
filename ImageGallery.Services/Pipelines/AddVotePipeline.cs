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

            if (votesSoFar.Count > 10)
            {
                // return error
                return null;
            }
            else if (votesSoFar.Count == 10)
            {
                return await next();
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
                return new AddVoteResponse { VotesLeft = 10 - votesSoFar.Count };
            }
        }
    }
}
