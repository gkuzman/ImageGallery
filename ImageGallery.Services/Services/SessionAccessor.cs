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
        public async Task InitializeSessionAsync()
        {
            await _httpContext.HttpContext.Session.LoadAsync();

            if (!_httpContext.HttpContext.Session.Keys.Any(x => string.Equals(x, Constants.Constants.USER_VOTES, StringComparison.OrdinalIgnoreCase)))
            {
                var voteList = new Dictionary<string, int>();
                _httpContext.HttpContext.Session.SetString(Constants.Constants.USER_VOTES, JsonConvert.SerializeObject(voteList));
            }
        }

        public async Task<string> GetSessionIdAsync()
        {
            await _httpContext.HttpContext.Session.LoadAsync();
            return _httpContext.HttpContext.Session.Id;
        }

        public async Task<T> ReadFromSessionStringAsync<T>(string key) where T : new()
        {
            await _httpContext.HttpContext.Session.LoadAsync();
            var fromSession = _httpContext.HttpContext.Session.GetString(key);
            if (string.IsNullOrEmpty(fromSession))
            {
                return new T();
            }

            return JsonConvert.DeserializeObject<T>(fromSession);
        }

        public async Task SetObjectToStringSessionAsync<T>(string key, T value) where T : new()
        {
            await _httpContext.HttpContext.Session.LoadAsync();
            _httpContext.HttpContext.Session.SetString(key, JsonConvert.SerializeObject(value));
        }
    }
}
