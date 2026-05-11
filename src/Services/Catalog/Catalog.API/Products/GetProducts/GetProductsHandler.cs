
namespace Catalog.API.Products.GetProducts
{
    // Este arquivo contém os registros de consulta e resultado, bem como o handler para o endpoint GetProducts..
    public record GetProductsQuery() : IQuery<GetProductsResult>;

    // O resultado da consulta GetProducts, que contém uma lista de produtos.
    public record GetProductsResult(IEnumerable<Product> Products);

    // O handler para a consulta GetProducts, que é responsável por processar a consulta e retornar o resultado.
    internal class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger) 
        : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductsQueryHandler.Handle called with {@Query}", query);

            var products = await session.Query<Product>().ToListAsync(cancellationToken);

            return new GetProductsResult(products);
        }
    }
}
