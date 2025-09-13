namespace Silo.API.Features.Buy_Products.DTOs;

public class CategoryDTO
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public IEnumerable<ProductDTO>? Products { get; set; } = [];
}

public class CategoryDTOMappigProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Category, CategoryDTO>();
    }
}