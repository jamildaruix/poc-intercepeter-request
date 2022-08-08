var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options =>
    {
         options.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowOrigin");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/api/validate", (SolicitacaoBV model) =>
{
    return new AprovadoBV(DateTime.Now, DateTime.Now.Second > 30);
});

app.Run();

internal record AprovadoBV(DateTime Date, bool Aprovado);

internal record SolicitacaoBV(int Codigo, decimal Valor);