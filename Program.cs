using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

using Npgsql;
using Microsoft.EntityFrameworkCore.Design;
using System.Threading.RateLimiting;
using VictoryCloudApi.Data;
using VictoryCloudApi.Util;
var builder = WebApplication.CreateBuilder(args);
var conString = builder.Configuration.GetConnectionString("devDb") ?? 
throw new InvalidOperationException("Connection string not found");
builder.Services.AddDbContext<MyDbContext>(options => options.UseNpgsql(conString));
try
{
    using var conn = new NpgsqlConnection(conString);
    conn.Open(); 
    Console.WriteLine("Successfully connected to PostgreSQL database.");
}
catch (Exception ex)
{
    Console.WriteLine($"Database connection failed: {ex.Message}");

}

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("v1", limiter =>
    {
        limiter.PermitLimit = 10;
        limiter.Window = TimeSpan.FromSeconds(10);
        limiter.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        limiter.QueueLimit = 2;
    });
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseSwagger();
app.UseSwaggerUI();
// app.UseHttpsRedirection();
app.MapControllers();
app.UseRateLimiter();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

var v1 = app.MapGroup("/api/v1").RequireRateLimiting("v1");




app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
