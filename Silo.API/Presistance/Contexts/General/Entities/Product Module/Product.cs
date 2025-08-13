

namespace Silo.API.Presistance.Contexts.General.Entities.Product_Module;

public class Product : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public DateTime CreatedOn { get; set; }

    public int CreatedById { get; set; }
    public required User CreatedBy { get; set; }

    public int CategoryId { get; set; }
    public required Category Category { get; set; } 
}