using Greet;
using Grpc.Core;
using MongoDB.Driver;
using Poc.Intercepeter.GRPC.Models;

namespace Poc.Intercepeter.GRPC.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        private readonly IMongoCollection<LogFornecedor> _collection;

        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;

            const string serverMongoDB = "mongodb";
            const string username = "root";
            const string password = "password";

            var uri = $"mongodb://{username}:{Uri.EscapeDataString(password)}@{serverMongoDB}:27017/";
            var url = new MongoUrl(uri);

            var mongoClient = new MongoClient(url);

            var mongoDatabase = mongoClient.GetDatabase($"{nameof(LogFornecedor)}Store");
            _collection = mongoDatabase.GetCollection<LogFornecedor>(nameof(LogFornecedor));
        }

        public override async Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {

            await _collection.InsertOneAsync(new LogFornecedor { Jamil = "teste" });

            return await Task.FromResult(new HelloReply
            {
                Message = $"gRPC Comunication {request.Name}, Datetime {DateTime.Now}"
            });
        }
    }
}