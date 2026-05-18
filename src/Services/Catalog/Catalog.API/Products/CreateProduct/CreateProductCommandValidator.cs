namespace Catalog.API.Products.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand> 
    {
        public CreateProductCommandValidator()
        {
            RuleFor(command => command.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(command => command.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(command => command.ImageFile).NotEmpty().WithMessage("ImageFile is required");
            RuleFor(command => command.Price).NotEmpty().WithMessage("Price must be grater than 0");
        }
    }
}
