using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviors
{

    public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) 
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull,IRequest<TResponse>
        where TResponse : notnull
    {
        // O método Handle é chamado para cada requisição que passa por este comportamento de pipeline.
        // Ele registra o início do processamento, mede o tempo gasto para processar a requisição e registra o final do processamento.
        // Se o tempo gasto for superior a 3 segundos, ele também registra um aviso de desempenho.
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("[START] Handle request={request} - Response={response} - RequestData={requestData}", 
                typeof(TRequest).Name, typeof(TResponse).Name, request);

            var timer = new Stopwatch();
            timer.Start();

            var response = await next();

            timer.Stop();

            var timeTaken = timer.Elapsed;

            if (timeTaken.Seconds > 3)
                logger.LogWarning("[PERFORMANCE] The request={request} took {timeTaken} seconds",
                    typeof(TRequest).Name, timeTaken.Seconds);

            logger.LogInformation("[END] Handled {request} with {response}",
                typeof(TRequest).Name, typeof(TResponse).Name);

            return response;
        }
    }
}
