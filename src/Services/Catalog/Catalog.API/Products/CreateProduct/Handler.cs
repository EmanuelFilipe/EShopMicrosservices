using MediatR;

namespace Catalog.API.Products.CreateProduct
{
    // representa dados necessarios para criar um produto
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : IRequest<CreateProductResult>;

    // represebta objeto de retorno do objeto de resposta
    public record CreateProductResult(Guid Id);

    internal class Handler : IRequestHandler<CreateProductCommand, CreateProductResult>
    {
        public Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
