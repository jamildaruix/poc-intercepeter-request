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

            var mongoClient = new MongoClient(@"mongodb://root:password@localhost:27017");

            var mongoDatabase = mongoClient.GetDatabase("LogForncedorStore");
            _collection = mongoDatabase.GetCollection<LogFornecedor>("teste");
        }

        public override async Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            try
            {
                await _collection.InsertOneAsync(new LogFornecedor { Jamil = "teste"});
            }
            catch (Exception ex)
            {

                throw;
            }
            

            return await Task.FromResult(new HelloReply
            {
                Message = $"gRPC Comunication {request.Name}, Datetime {DateTime.Now}"
            });
        }
    }
}