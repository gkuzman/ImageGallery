using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

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
