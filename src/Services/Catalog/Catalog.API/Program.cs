var builder = WebApplication.CreateBuilder(args);

//add services to container

builder.Services.AddCarter();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("PostgreSQLDatabase")!);
}).UseLightweightSessions(); // LightweightSessions para melhor performance

var app = builder.Build();

app.MapCarter();

//configure http request pipeline 

app.Run();
