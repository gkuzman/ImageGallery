using ImageGallery.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ImageGallery.Services.Services
{
    public class DatabasesSyncService : IDatabasesSyncService
    {
        private readonly HttpClient _httpClient;

        public DatabasesSyncService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task SyncImageGallery()
        {
            var e = await _httpClient.GetAsync("/weatherForecast");
            var content = await e.Content.ReadAsStringAsync();
        }
    }
}
