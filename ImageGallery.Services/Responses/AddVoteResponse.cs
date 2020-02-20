using ImageGallery.Shared.Response;

namespace ImageGallery.Services.Responses
{
    public class AddVoteResponse : ResponseBase
    {
        public int VotesLeft { get; set; }

        public bool VotingCompleted { get; set; }
    }
}
