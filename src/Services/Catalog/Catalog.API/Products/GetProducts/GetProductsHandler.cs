
using Marten.Pagination;

namespace Catalog.API.Products.GetProducts
{
    // Este arquivo contém os registros de consulta e resultado, bem como o handler para o endpoint GetProducts..
    public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductsResult>;

    // O resultado da consulta GetProducts, que contém uma lista de produtos.
    public record GetProductsResult(IEnumerable<Product> Products);

    // O handler para a consulta GetProducts, que é responsável por processar a consulta e retornar o resultado.
    internal class GetProductsQueryHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>().ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);

            return new GetProductsResult(products);
        }
    }
}
