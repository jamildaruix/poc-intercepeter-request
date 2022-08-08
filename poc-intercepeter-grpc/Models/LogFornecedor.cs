using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Poc.Intercepeter.GRPC.Models
{
    public record LogFornecedor(BsonDocument documentRequest) : BsonBase;

    public record BsonBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
    }
}
