using BuildingBlocks.Messaging.MassTransit;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// add services to the container

#region [ Application Services]

var assembly = typeof(Program).Assembly;

builder.Services.AddCarter();

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

#endregion

#region [ Data Services]

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("PostgreSQLDatabase")!);
    // configura que UserName será o PK da tabela ShoppingCart
    opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

#endregion

#region [ gRPC Services ]

builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
}).ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };

    return handler;
});

#endregion

// Async Communication Services
builder.Services.AddMessageBroker(builder.Configuration, Assembly.GetExecutingAssembly());

#region [ Cross-Cutting Services ]

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
                .AddNpgSql(builder.Configuration.GetConnectionString("PostgreSQLDatabase")!)
                .AddRedis(builder.Configuration.GetConnectionString("Redis")!);

#endregion

var app = builder.Build();

// configure http request pipeline
app.MapCarter();

// usado junto com o AddExceptionHandler
app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
