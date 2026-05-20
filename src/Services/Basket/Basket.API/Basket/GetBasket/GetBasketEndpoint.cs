


namespace Basket.API.Basket.GetBasket
{
    //    public record GetBasketResult(ShoppingCart Cart);
    public record GetBasquetResponse(ShoppingCart Cart);

    public class GetBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
            {
                var result = await sender.Send(new GetBasquetQuery(userName));

                var response = result.Adapt<GetBasquetResponse>();
                return Results.Ok(response);
            })
            .WithName("Get Basket")
            .Produces<GetBasquetResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get basket")
            .WithDescription("Retrieve a user's shopping basket");
        }
    }
}
