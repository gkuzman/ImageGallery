using ImageGallery.Shared.Extensions;
using ImageGallery.Shared.Response;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ImageGallery.Shared.Pipelines
{
    public class ProcessingPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : ResponseBase, new()
    {
        private readonly IPipelineBehavior<TRequest, TResponse> _inner;
        private readonly ILogger<ProcessingPipeline<TRequest, TResponse>> _logger;

        public ProcessingPipeline(IPipelineBehavior<TRequest, TResponse> inner, ILogger<ProcessingPipeline<TRequest, TResponse>> logger)
        {
            _inner = inner;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = new TResponse();
            try
            {
                response = await _inner.Handle(request, cancellationToken, next);
                return response;
            }
            catch (Exception ex)
            {
                response.ErrorMessages.Add($"An exception occured while processing your request: {ex.Message}");

                return response;
            }
            finally
            {
                if (response.HasErrors)
                {
                    _logger.LogError($"An error(s) have occured {response.ErrorMessages.GetErrorMessagesFormated()}");
                }
            }
        }
    }
}
