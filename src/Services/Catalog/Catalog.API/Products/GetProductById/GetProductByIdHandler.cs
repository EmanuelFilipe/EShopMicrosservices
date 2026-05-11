

namespace Catalog.API.Products.GetProductById
{
    // Este arquivo contém os registros de query e result, bem como o handler para o endpoint GetProductById.
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

    // O resultado da query GetProductById, que contém um produto específico.
    public record GetProductByIdResult(Product Product);

    internal class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByIdQueryHandler.Handle called with {@Query}", query);

            var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

            if (product is null)
            {
                throw new ProductNotFoundException();
            }

            return new GetProductByIdResult(product);
        }
    }
}
