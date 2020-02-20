using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageGallery.Services.Requests;
using ImageGallery.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImageGallery.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VotesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VotesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPut]
        public async Task<IActionResult> Add([FromBody]AddVoteRequest request)
        {
            var result = await _mediator.Send(request);

            if (result.HasErrors)
            {
                return BadRequest(new { Message = result.ErrorMessages.GetErrorMessagesFormated() });
            }

            return Ok(result);
        }
    }
}