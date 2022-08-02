using Flurl;
using Flurl.Http;
using Poc.Intercepeter.Api.Domain.Credit;
using Poc.Intercepeter.Api.HttpHandler;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Poc.Intercepeter.Api.Service.BV
{
    public class ApproveCreditService : IApproveCreditService
    {
        private readonly HttpClient _httpClient;

        public ApproveCreditService(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<bool> InvokeAsync(CreditRequest creditRequest)
        {
            var returns = false;

            try
            {
                HttpRequestMessage httpRequestMessage = CreateRequestMessage();
                var response = await _httpClient.SendAsync(httpRequestMessage);
                var jsonString = await response.Content.ReadAsStringAsync();
                var weatherForecast = JsonSerializer.Deserialize<AprovadoVBResponse>(jsonString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                returns = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returns;
        }

        private static HttpRequestMessage CreateRequestMessage()
                        => new(HttpMethod.Get, "http://host.docker.internal:64889/api/validate")
                        {
                            Headers = { { "Accept", "application/json" } }
                        };

        //private static HttpRequestMessage CreateRequestMessage()
        //    => new(HttpMethod.Get, "https://viacep.com.br/ws/01001000/json/")
        //    {
        //        Headers = { { "Accept", "application/json" } }
        //    };
    }
}
