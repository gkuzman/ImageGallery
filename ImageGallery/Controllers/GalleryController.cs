using ImageGallery.Services.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ImageGallery.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IMediator _mediator;

        public GalleryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public IActionResult Index()
        {
            _mediator.Send(new GalleryLoadRequest());
            return View();
        }
    }
}