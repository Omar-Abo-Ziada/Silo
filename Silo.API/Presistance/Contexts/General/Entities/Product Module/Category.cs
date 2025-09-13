namespace Silo.API.Presistance.Contexts.General.Entities.Product_Module;

public class Category : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public ICollection<Product> Products { get; set; } = [];
}