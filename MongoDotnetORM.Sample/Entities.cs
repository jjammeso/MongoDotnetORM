using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using src;

namespace MongoDotnetORM.Sample
{
    // Define your entities
    [Entity("users")]
    public class User : BaseEntity
    {
        [Column("username")]
        public required string Username { get; set; }
        public required string Email { get; set; }
        public int Age { get; set; }
        public List<string>? Tags { get; set; }
    }

    // Alternative without BaseEntity
    [Entity("products")]
    public class Product
    {
        [PrimaryKey]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }

        public required string Name { get; set; }
        public decimal Price { get; set; }

        [CreatedDate]
        public DateTime CreatedAt { get; set; }

        [UpdatedDate]
        public DateTime UpdatedAt { get; set; }
    }
}
