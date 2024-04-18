using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CarDealerShopAPI.Models
{
    public class Car
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public double Price { get; set; }
    }
}
