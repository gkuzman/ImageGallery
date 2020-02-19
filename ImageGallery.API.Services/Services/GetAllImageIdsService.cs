using ImageGallery.API.Services.Requests;
using ImageGallery.API.Services.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageGallery.API.Services.Services
{
    public class GetAllImageIdsService : IRequestHandler<GetAllImageIdsRequest, GetAllImageIdsResponse>
    {
        public async Task<GetAllImageIdsResponse> Handle(GetAllImageIdsRequest request, CancellationToken cancellationToken)
        {
            var response = new GetAllImageIdsResponse();
            response.ImageIds.Add("test");
            return response;
        }
    }
}
