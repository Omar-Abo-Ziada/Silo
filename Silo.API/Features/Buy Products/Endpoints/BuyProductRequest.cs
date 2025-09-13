namespace Silo.API.Features.Endpoints.Buy_Products;

public record BuyProductRequest(int productId, int quantity);

public class BuyProductResponseValidator : AbstractValidator<BuyProductRequest>
{
    public BuyProductResponseValidator()
    {
        RuleFor(x => x.productId)
            .NotEmpty().WithMessage("Product ID is required.")
            .GreaterThan(0).WithMessage("Product ID must be greater than 0.");

        RuleFor(x => x.quantity)
            .NotEmpty().WithMessage("Quantity is required.")
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
    }
}