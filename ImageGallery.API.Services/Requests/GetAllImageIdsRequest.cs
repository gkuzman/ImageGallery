using ImageGallery.API.Services.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageGallery.API.Services.Requests
{
    public class GetAllImageIdsRequest : IRequest<GetAllImageIdsResponse>
    {
    }
}
