using Log;

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
        var response = await base.SendAsync(request, cancellationToken);
        _ = InvokeApiLogAsync(request, response);
        return response;
    }

    protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        HttpResponseMessage response = base.Send(request, cancellationToken);
        _ = InvokeApiLogAsync(request, response);
        return response;
    }

    private async Task InvokeApiLogAsync(HttpRequestMessage httpRequestMessage, HttpResponseMessage httpResponseMessage)
    {
        var response = await httpResponseMessage.Content.ReadAsStringAsync();

        if (!string.IsNullOrWhiteSpace(response))
        {
            //ArgumentNullException.ThrowIfNull(results, nameof(httpResponseMessage));

            FornecedorRequest fornecedorRequest = new()
            { 
                RequestForncedor = await httpRequestMessage.Content!.ReadAsStringAsync(),
                ResponseForncedor  = response
            };

            var reply = await _client.CreateAsync(fornecedorRequest);

            Console.WriteLine(reply.Message);
        }
    }

}