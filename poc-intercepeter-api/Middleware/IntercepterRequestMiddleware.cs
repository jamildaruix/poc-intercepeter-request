using Poc.Intercepeter.Api.Domain.Credit;
using System.Text;
using System.Text.Json;

namespace Poc.Intercepeter.Api.Middleware
{
    public class IntercepterRequestMiddleware
    {
        private readonly RequestDelegate _next;

        public IntercepterRequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //Armazenar no MONGO DB O Request e Response de envio

            var request = context.Request;

            try
            {

                request.EnableBuffering();

                request.Body.Position = 0;

                using (StreamReader reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
                {
                    var jsonRequest = await reader.ReadToEndAsync();

                    if (!string.IsNullOrEmpty(jsonRequest))
                    {
                        var weatherForecast = JsonSerializer.Deserialize<CreditRequest>(jsonRequest, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                        Console.WriteLine($"Dados Json {weatherForecast!.Name}");
                    }
                }

            }
            finally
            {

                request.Body.Position = 0;
            }

            _next(context);
        }
    }
}
