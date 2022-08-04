using Log;
using System.Text.Json;

namespace Poc.Intercepeter.Api.HttpHandler;

public class IntercepeterHandler : DelegatingHandler
{
    private readonly LogRequest.LogRequestClient _client;

    public IntercepeterHandler(LogRequest.LogRequestClient client)
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
            var creditAproved = JsonSerializer.Deserialize<AprovadoVBResponse>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            //var reply = await _client.SayHelloAsync(new HelloRequest { Name = $"SOLICITACAO {aprovadoCredit!.Aprovado}" });
            FornecedorRequest fornecedorRequest = new()
            { 
                PayloadApi = $"PALYLOADAPI  {DateTime.Now}",
                RequestForncedor = $"REQUEST FORNCEDOR  {DateTime.Now}",
                RequestId = $"request id {DateTime.Now}",
                ResponseForncedor  = response
            };

            var reply = await _client.CreateAsync(fornecedorRequest);

            Console.WriteLine(reply.Message);
        }
    }

}