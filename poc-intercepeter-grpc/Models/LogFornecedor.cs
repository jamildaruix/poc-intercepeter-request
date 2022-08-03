using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Poc.Intercepeter.GRPC.Models
{
    //public record LogFornecedor(string RequestId, string PayloadApi, string RequestForncedor, string ResponseForncedor) : BsonBase;

    public class LogFornecedor : BsonBase
    {
        public string Jamil { get; set; }
    }

    public class BsonBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
    }
}
