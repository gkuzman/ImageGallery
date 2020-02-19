using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageGallery.API.DAL.Entities
{
    public class ImageDBO
    {
        [BsonId]
        public ObjectId InternalId { get; set; }
        public string Id { get; set; }
        public byte[] Content { get; set; }
    }
}
