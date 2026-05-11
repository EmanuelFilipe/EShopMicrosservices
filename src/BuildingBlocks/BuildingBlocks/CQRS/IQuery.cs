using MediatR;

namespace BuildingBlocks.CQRS
{
    // representa uma consulta que pode retornar um resultado específico do tipo TResponse não pode ser nulo
    public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull
    {
    }
}
