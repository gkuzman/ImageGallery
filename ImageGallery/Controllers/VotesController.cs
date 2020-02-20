using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageGallery.Services.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImageGallery.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VotesController : ControllerBase
    {
        [HttpPut]
        public async Task<IActionResult> Add([FromBody]VoteRequest request)
        {
            return BadRequest(new { Message = "sta da radim" });
        }
    }
}