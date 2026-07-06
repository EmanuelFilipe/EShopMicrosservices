using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddRateLimiter(rateLimiterOptions =>
{
   rateLimiterOptions.AddFixedWindowLimiter("fixed", options =>
    {
        // em 10 segundos, apenas 5 requisições serão permitidas
        options.Window = TimeSpan.FromSeconds(10);
        options.PermitLimit = 5;
        //options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        //options.QueueLimit = 2;
    });
});

// Configure the HTTP request pipeline.
var app = builder.Build();

app.UseRateLimiter();
app.MapReverseProxy();


app.Run();
