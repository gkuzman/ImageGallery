using ImageGallery.Services.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ImageGallery.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly ILogger<GalleryController> _logger;
        private readonly IMediator _mediator;

        public GalleryController(IHttpContextAccessor httpContext, ILogger<GalleryController> logger, IMediator mediator)
        {
            _httpContext = httpContext;
            _logger = logger;
            _mediator = mediator;
        }
        public IActionResult Index()
        {
            _httpContext.HttpContext.Session.SetString("startedVoting", "true");
            _logger.LogError($"Voting started for user with session id: {_httpContext.HttpContext.Session.Id}");
            _mediator.Send(new GalleryLoadRequest());
            return View();
        }
    }
}