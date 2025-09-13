

namespace Silo.API.Presistance.Contexts.General.Entities.Product_Module;

public class Product : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public int? CategoryId { get; set; }
    public Category? Category { get; set; }
}