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
    public class GalleryLoadPipeline : IPipelineBehavior<GalleryLoadRequest, GalleryLoadResponse>
    {
        public async Task<GalleryLoadResponse> Handle(GalleryLoadRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<GalleryLoadResponse> next)
        {
            return await next();
        }
    }
}
