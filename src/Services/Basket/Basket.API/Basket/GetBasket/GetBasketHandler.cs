namespace Basket.API.Basket.GetBasket
{
    public record GetBasquetQuery(string UserName) : IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCart Cart);

    internal class GetBasketQueryHandler(IBasketRepository repository) : IQueryHandler<GetBasquetQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasquetQuery query, CancellationToken cancellationToken)
        {
            var basket = await repository.GetBasket(query.UserName);
            return new GetBasketResult(basket);
        }
    }
}
