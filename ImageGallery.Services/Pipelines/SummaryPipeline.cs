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
            if (!await _session.ReadFromSessionString<bool>("votingDone"))
            {
                response.ShowSummary = false;
            }
            else
            {
                response = await next();
            }

            return response;
        }
    }
}
