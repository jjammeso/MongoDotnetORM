using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDotnetORM
{
    public abstract class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
