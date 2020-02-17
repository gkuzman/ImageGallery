using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImageGallery.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IHttpContextAccessor _httpContext;

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