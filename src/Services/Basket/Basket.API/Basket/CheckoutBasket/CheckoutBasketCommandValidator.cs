namespace Basket.API.Basket.CheckoutBasket;

public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidator()
    {
        RuleFor(x => x.BasketCheckoutDTO).NotNull().WithMessage("BasketCheckoutDTO can´t be null");
        RuleFor(x => x.BasketCheckoutDTO.UserName).NotNull().WithMessage("UserName is required");
    }
}
