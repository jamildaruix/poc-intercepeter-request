using Grpc.Core;
using Log;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Poc.Intercepeter.GRPC.Models;

namespace Poc.Intercepeter.GRPC.Services
{
    public class LoForncedorService : LogRequest.LogRequestBase
    {
        private readonly ILogger<LoForncedorService> _logger;
        private readonly IMongoCollection<LogFornecedor> _collection;
        private const string serverMongoDB = "mongodb";
        private const string username = "root";
        private const string password = "password";

        public LoForncedorService(ILogger<LoForncedorService> logger)
        {
            _logger = logger;

            var uri = $"mongodb://{username}:{password}@{serverMongoDB}:27017/";
            var url = new MongoUrl(uri);

            var mongoClient = new MongoClient(url);

            var mongoDatabase = mongoClient.GetDatabase($"{nameof(LogFornecedor)}Store");
            _collection = mongoDatabase.GetCollection<LogFornecedor>(nameof(LogFornecedor));
        }

        public override Task<StatusResponse> Create(FornecedorRequest request, ServerCallContext context)
        {
            var requestFornecedor = new BsonDocument();
            requestFornecedor.Add("payloadApi", request.PayloadApi);
            requestFornecedor.Add("requestFornecedor", request.RequestForncedor);
            requestFornecedor.Add("responseForncedor", BsonSerializer.Deserialize<BsonDocument>(request.ResponseForncedor));

            _collection.InsertOneAsync(new LogFornecedor(request.RequestId, requestFornecedor));

            return Task.FromResult(new StatusResponse
            {
                Message = $"gRPC Comunication {request.RequestId}, Datetime {DateTime.Now}"
            });
        }
    }
}