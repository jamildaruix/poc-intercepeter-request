using Grpc.Net.Client;
using System.Text.Json;

namespace Poc.Intercepeter.Api.HttpHandler;

public class IntercepeterHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var returns = await base.SendAsync(request, cancellationToken);
        _ = InvokeApiLog(returns);
        return returns;
    }

    protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        HttpResponseMessage returns = base.Send(request, cancellationToken);
        _ = InvokeApiLog(returns);
        return returns;
    }

    private async Task InvokeApiLog(HttpResponseMessage httpResponseMessage)
    {
        ///var results = await(await httpResponseMessage).Content.ReadAsStringAsync();

        var channel = GrpcChannel.ForAddress("https://localhost:5001");
        var client = new Greeter.GreeterClient(channel);

        var response = await client.SayHelloAsync(
            new HelloRequest { Name = "World" });

        Console.WriteLine(response.Message);


        var response = await httpResponseMessage.Content.ReadAsStringAsync();

        if (!string.IsNullOrWhiteSpace(response))
        {
            //ArgumentNullException.ThrowIfNull(results, nameof(httpResponseMessage));
            var aprovadoCredit = JsonSerializer.Deserialize<AprovadoVBResponse>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }

}