using Greet;
using Grpc.Core;

namespace Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context) 
            => Task.FromResult(new HelloReply
        {
            Message = $"gRPC Comunication {request.Name}, Datetime {DateTime.Now}"
        });
    }
}