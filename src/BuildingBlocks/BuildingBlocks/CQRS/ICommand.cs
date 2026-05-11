using MediatR;

namespace BuildingBlocks.CQRS
{
    // devolve uma resposta vazia, ou seja, é um comando que não retorna nenhum resultado específico
    public interface ICommand : ICommand<Unit> { }

    // representa um comando que pode retornar um resultado específico do tipo TResponse
    public interface ICommand<out TResponse> : IRequest<TResponse> { }
}
