using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviors
{
    /// <summary>
    /// Comportamento de pipeline do MediatR responsável por validar comandos antes de chegarem ao handler. 
    /// Recebe todos os <see cref="IValidator{TRequest}"/> registrados para o tipo de comando 
    /// <typeparamref name="TRequest"/> e executa  suas regras de validação. 
    /// Caso existam falhas, lança uma <see cref="ValidationException"/>; caso contrário, permite que 
    /// a requisição siga para o próximo passo do pipeline.
    /// </summary>
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) 
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse> // Isso garante que só comandos (não queries, por exemplo) passem por esse pipeline.
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // cria o objeto de contexto de validação para o Command atual
            var context = new ValidationContext<TRequest>(request);

            // Executa a validação de forma assíncrona para todos os validadores registrados.
            var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            // Coleta todas as falhas de validação, se existirem.
            var failures = validationResults.Where(r => r.Errors.Any())
                                            .SelectMany(r => r.Errors).ToList();

            // Lança uma exceção se houver falhas de validação.
            if (failures.Any()) throw new ValidationException(failures);

            // Permite que a requisição siga para o próximo passo do pipeline (normalmente o handler).
            return await next(); 
        }

    }
}
