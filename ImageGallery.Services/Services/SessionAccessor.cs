using ImageGallery.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGallery.Services.Services
{
    public class SessionAccessor : ISessionAccessor
    {
        private readonly IHttpContextAccessor _httpContext;

        public SessionAccessor(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public async Task InitializeSession()
        {
            await _httpContext.HttpContext.Session.LoadAsync();

            if (!_httpContext.HttpContext.Session.Keys.Any(x => string.Equals(x, "votes", StringComparison.OrdinalIgnoreCase)))
            {
                var voteList = new List<string>();
                _httpContext.HttpContext.Session.SetString("votes", JsonConvert.SerializeObject(voteList));
            }
        }
    }
}
