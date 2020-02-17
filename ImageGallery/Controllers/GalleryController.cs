using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ImageGallery.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly ILogger<GalleryController> _logger;

        public GalleryController(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public IActionResult Index()
        {
            _httpContext.HttpContext.Session.SetString("startedVoting", "true");
            return View();
        }
    }
}