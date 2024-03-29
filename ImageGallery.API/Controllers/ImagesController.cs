﻿using System.Threading.Tasks;
using ImageGallery.API.Services.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ImageGallery.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ImagesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ImagesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> AllIds()
        {
            var result = await _mediator.Send(new GetAllImageIdsRequest());
            return Ok(result.ImageIds);
        }

        [HttpGet("{imageId}")]
        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Client)]
        public async Task<IActionResult> GetImage(string imageId)
        {
            var result = await _mediator.Send(new GetImageRequest { ImageId = imageId });
            return new FileContentResult(result.Content, "image/jpeg");
        }
    }
}
