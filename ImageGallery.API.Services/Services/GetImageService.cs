using ImageGallery.API.DAL.Services;
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
    public class GetImageService : IRequestHandler<GetImageRequest, GetImageResponse>
    {
        private readonly ImageService _imageService;

        public GetImageService(ImageService imageService)
        {
            _imageService = imageService;
        }
        public async Task<GetImageResponse> Handle(GetImageRequest request, CancellationToken cancellationToken)
        {
            var response = new GetImageResponse();
            var image = await _imageService.GetImage(request.ImageId);

            if (image is null)
            {
                response.ErrorMessages.Add("Could not retrieve image from MongoDB");
                return response;
            }
            response.ImageId = image?.Id;
            response.Content = image?.Content;

            return response;
        }
    }
}
