namespace Silo.API.Features.Buy_Products.DTOs;

public class ProductDTO
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public int? CategoryId { get; set; }
    public CategoryDTO? Category { get; set; }
}

public class ProductDTOMappigProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Product, ProductDTO>();
    }
}