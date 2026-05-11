namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductRequest(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price);
    public record UpdateProductResponse(bool IsSuccess);

    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products/", async (UpdateProductRequest request, ISender sender) =>
            {
                // converte a solicitação para o formato de comando esperado pelo MediatR
                var command = request.Adapt<UpdateProductCommand>();

                // envia o comando para o MediatR e aguarda a resposta
                var result = await sender.Send(command);

                // converte o resultado para o formato de resposta esperado pela API
                var response = new UpdateProductResponse(result.IsSuccess);

                return Results.Ok(response);
            })
            .WithName("UpdateProduct")
            .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Update product")
            .WithDescription("Update Product");
        }
    }
}
