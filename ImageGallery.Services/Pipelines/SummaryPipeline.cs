using ImageGallery.Services.Interfaces;
using ImageGallery.Services.Requests;
using ImageGallery.Services.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ImageGallery.Services.Pipelines
{
    public class SummaryPipeline : IPipelineBehavior<SummaryRequest, SummaryResponse>
    {
        private readonly ISessionAccessor _session;

        public SummaryPipeline(ISessionAccessor session)
        {
            _session = session;
        }
        public async Task<SummaryResponse> Handle(SummaryRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<SummaryResponse> next)
        {
            var response = new SummaryResponse();
            if (!await _session.ReadFromSessionStringAsync<bool>(Constants.Constants.VOTING_DONE))
            {
                response.ShowSummary = false;
            }
            else
            {
                request.UserId = await _session.GetSessionIdAsync();
                response = await next();
            }

            return response;
        }
    }
}
