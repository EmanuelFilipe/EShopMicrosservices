namespace Catalog.API.Products.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("Product ID is required");

            RuleFor(command => command.Name).NotEmpty()
                                            .WithMessage("Name is required")
                                            .Length(2, 150).WithMessage("Name musb be between 2 and 150 chacarters");

            RuleFor(command => command.Price).GreaterThan(0).WithMessage("Price must be grater than 0");
        }
    }
}
