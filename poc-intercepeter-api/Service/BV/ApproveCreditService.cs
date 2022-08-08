using Poc.Intercepeter.Api.Domain.Credit;
using Poc.Intercepeter.Api.HttpHandler;
using System.Text;
using System.Text.Json;

namespace Poc.Intercepeter.Api.Service.BV
{
    public class ApproveCreditService : IApproveCreditService
    {
        private readonly HttpClient _httpClient;

        public ApproveCreditService(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<bool> InvokeAsync(CreditRequest creditRequest)
        {
            try
            {
                var jsonCreditRequest = JsonSerializer.Serialize(creditRequest, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });

                HttpRequestMessage httpRequestMessage = CreateRequestMessage(jsonCreditRequest);
                var response = await _httpClient.SendAsync(httpRequestMessage);
                
                if (!response.IsSuccessStatusCode)
                    return false;

                var jsonString = await response.Content.ReadAsStringAsync();
                var requestBVExemplo = JsonSerializer.Deserialize<AprovadoVBResponse>(jsonString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static HttpRequestMessage CreateRequestMessage(string json)
                        => new(HttpMethod.Post, "http://host.docker.internal:64739/api/validate")
                        {
                            Headers = { { "Accept", "application/json" } },
                            Content = new StringContent(json, Encoding.UTF8, "application/json")
                        };

        //private static HttpRequestMessage CreateRequestMessage()
        //    => new(HttpMethod.Get, "https://viacep.com.br/ws/01001000/json/")
        //    {
        //        Headers = { { "Accept", "application/json" } }
        //    };
    }
}
