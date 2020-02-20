using ImageGallery.Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageGallery.Services.Responses
{
    public class AddVoteResponse : ResponseBase
    {
        public int VotesLeft { get; set; }
    }
}
