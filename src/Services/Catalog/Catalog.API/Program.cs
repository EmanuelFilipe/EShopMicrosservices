using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

//add services to container

var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);

    // Registra o comportamento de validação para todos os comandos (ICommand<TResponse>) usando o MediatR.
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

// Adiciona os validadores do FluentValidation
builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("PostgreSQLDatabase")!);
}).UseLightweightSessions(); // LightweightSessions para melhor performance

// Inicializa o banco de dados com dados iniciais apenas em ambiente de desenvolvimento,
// utilizando a classe CatalogInitialData para popular o banco.
if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitialData>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
                .AddNpgSql(builder.Configuration.GetConnectionString("PostgreSQLDatabase")!);

var app = builder.Build();

app.MapCarter();

// ativa o uso do middleware CustomExceptionHandler
app.UseExceptionHandler(opt => { });

// Configura o endpoint de health check para "/health",
// utilizando o UIResponseWriter para formatar a resposta de saúde de forma amigável para a interface do usuário do HealthChecks.UI.
app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
    

// Logica movida para BuildingBlocks.CustomExceptionHandler
// Configura o middleware de tratamento de exceções para capturar e lidar com erros que ocorrem durante o processamento das solicitações HTTP.
//app.UseExceptionHandler(exceptionHandlerApp =>
//{
//    exceptionHandlerApp.Run(async context =>
//    {
//        // Obtém a exceção que ocorreu durante o processamento da solicitação.
//        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
//        if (exception is null) return;

//        // Cria um objeto ProblemDetails para fornecer detalhes sobre o erro ocorrido.
//        var problemDetails = new ProblemDetails
//        {
//            Title = exception.Message,
//            Status = StatusCodes.Status500InternalServerError,
//            Detail = exception.StackTrace
//        };

//        // Registra o erro usando o logger, associando a exceção e sua mensagem.
//        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
//        logger.LogError(exception, exception.Message);

//        // Configura a resposta HTTP para indicar que ocorreu um erro interno no servidor, definindo o status code
//        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

//        // Define o tipo de conteúdo da resposta como "application/problem+json", indicando que a resposta contém detalhes sobre o problema ocorrido.
//        context.Response.ContentType = "application/problem+json";

//        // Escreve a resposta JSON contendo os detalhes do problema usando o método WriteAsJsonAsync, serializando o objeto ProblemDetails para o formato JSON.
//        await context.Response.WriteAsJsonAsync(problemDetails);
//    });
//});

//configure http request pipeline 

app.Run();
