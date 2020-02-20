using ImageGallery.Services.Requests;
using ImageGallery.Services.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageGallery.Services.Services
{
    public class AddVoteService : IRequestHandler<AddVoteRequest, AddVoteResponse>
    {
        public async Task<AddVoteResponse> Handle(AddVoteRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
