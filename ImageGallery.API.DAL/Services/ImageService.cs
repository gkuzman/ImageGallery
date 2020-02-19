﻿using ImageGallery.API.DAL.Entities;
using ImageGallery.API.DAL.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImageGallery.API.DAL.Services
{
    public class ImageService
    {
        private readonly IMongoCollection<ImageDBO> _images;

        public ImageService(IOptions<ImageDatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var db = client.GetDatabase(settings.Value.DatabaseName);
            _images = db.GetCollection<ImageDBO>(settings.Value.ImagesCollectionName);
        }

        public async Task<IEnumerable<string>> GetAllIds()
        {
            return await _images.Find(image => true).Project(new ProjectionDefinitionBuilder<ImageDBO>().Expression(x => x.Id)).ToListAsync();
        }
    }
}