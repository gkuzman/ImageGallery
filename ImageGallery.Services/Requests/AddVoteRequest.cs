using ImageGallery.Services.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageGallery.Services.Requests
{
    public class AddVoteRequest : IRequest<AddVoteResponse>
    {
        public string ImageId { get; set; }

        public int Mark { get; set; }
    }
}
