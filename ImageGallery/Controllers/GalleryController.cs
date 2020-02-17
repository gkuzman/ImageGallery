using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ImageGallery.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly ILogger<GalleryController> _logger;

        public GalleryController(IHttpContextAccessor httpContext, ILogger<GalleryController> logger)
        {
            _httpContext = httpContext;
            _logger = logger;
        }
        public IActionResult Index()
        {
            _httpContext.HttpContext.Session.SetString("startedVoting", "true");
            _logger.LogError($"Voting started for user with session id: {_httpContext.HttpContext.Session.Id}");
            return View();
        }
    }
}