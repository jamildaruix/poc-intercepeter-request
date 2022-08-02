using Greet;
using Grpc.Net.Client;
using System.Text.Json;

namespace Poc.Intercepeter.Api.HttpHandler;

public class IntercepeterHandler : DelegatingHandler
{
    private readonly Greeter.GreeterClient _client;

    public IntercepeterHandler(Greeter.GreeterClient client)
    {
        _client = client;
    }

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
        
        var response = await httpResponseMessage.Content.ReadAsStringAsync();

        if (!string.IsNullOrWhiteSpace(response))
        {
            //ArgumentNullException.ThrowIfNull(results, nameof(httpResponseMessage));
            var aprovadoCredit = JsonSerializer.Deserialize<AprovadoVBResponse>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            var reply = await _client.SayHelloAsync(new HelloRequest { Name = $"SOLICITACAO {aprovadoCredit!.Aprovado}" });
            Console.WriteLine(reply.Message);
        }
    }

}