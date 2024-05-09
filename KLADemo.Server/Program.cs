using KLADemo.Contracts;
using KLADemo.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<INumericValidator, NumericValidator>();
builder.Services.AddTransient<INumericConverter, NumericConverter>();
builder.Services.AddTransient<INumericService, NumericService>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/convert", (INumericService service, [FromQuery] string number) =>
{
    try
    {
        var result = service.ConvertToStringRepresentation(number);
        return Results.Ok(result);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(ex.Message);
    }
})
.WithName("Converts To String Representation")
.WithOpenApi();

app.MapFallbackToFile("/index.html");

app.Run();
