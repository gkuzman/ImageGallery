﻿using ImageGallery.Services.Interfaces;
using ImageGallery.Services.Requests;
using ImageGallery.Services.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ImageGallery.Services.Pipelines
{
    public class GalleryLoadPipeline : IPipelineBehavior<GalleryLoadRequest, GalleryLoadResponse>
    {
        private readonly ISessionAccessor _sessionAccessor;

        public GalleryLoadPipeline(ISessionAccessor sessionAccessor)
        {
            _sessionAccessor = sessionAccessor;
        }
        public async Task<GalleryLoadResponse> Handle(GalleryLoadRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<GalleryLoadResponse> next)
        {
            await _sessionAccessor.InitializeSession();
            return await next();
        }
    }
}
