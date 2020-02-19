using ImageGallery.Services.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ImageGallery.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IMediator _mediator;

        public GalleryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("Gallery/Index/{pageNumber?}")]
        public async Task<IActionResult> Index(int pageNumber)
        {
            var result = await _mediator.Send(new GalleryLoadRequest(pageNumber));
            return View();
        }
    }
}