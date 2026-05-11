
using MediatR;

namespace BuildingBlocks.CQRS
{
    // representa um manipulador de consulta que processa consultas do tipo TQuery e retorna uma resposta do tipo
    // TResponse que não pode ser nula
    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
        where TResponse : notnull
    {
    }
}
