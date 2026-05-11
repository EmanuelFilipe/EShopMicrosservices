using MediatR;

namespace BuildingBlocks.CQRS
{
    // representa um manipulador de comando que processa comandos do tipo TCommand e não retorna nenhum resultado específico
    public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, Unit> 
        where TCommand : ICommand<Unit>
    {
    }

    // representa um manipulador de comando que processa comandos do tipo TCommand e retorna uma resposta do tipo
    // TResponse que não pode ser nula
    public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse> 
        where TCommand : ICommand<TResponse>
        where TResponse : notnull
    {
    }
}
