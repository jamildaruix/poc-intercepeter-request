using Greet;
using Poc.Intercepeter.Api.Domain.Credit;
using Poc.Intercepeter.Api.HttpHandler;
using Poc.Intercepeter.Api.Middleware;
using Poc.Intercepeter.Api.Service.BV;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGrpcClient<Greeter.GreeterClient>(c =>
{
    c.Address = new Uri("http://host.docker.internal:62395");
});

builder.Services
    .AddScoped<ICreditDomain, CreditDomain>();

//builder.Services.AddScoped<IApproveCreditService, ApproveCreditService>();

builder.Services
        .AddHttpClient<IApproveCreditService, ApproveCreditService>()
        .AddHttpMessageHandler<IntercepeterHandler>();


builder.Services.AddTransient<IntercepeterHandler>();
builder.Services.AddHttpClient(nameof(IntercepeterHandler))
                .AddHttpMessageHandler<IntercepeterHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<IntercepterRequestMiddleware>();
    
app.UseHttpsRedirection()
    .UseAuthorization();
    
app.MapControllers();
app.Run();
