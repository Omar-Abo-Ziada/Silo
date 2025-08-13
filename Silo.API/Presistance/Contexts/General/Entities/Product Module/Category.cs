using Silo.API.Presistance.Contexts.General.Entities.Common;

namespace Silo.API.Presistance.Contexts.General.Entities.Product_Module;

public class Category : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public int CreatedById { get; set; }
    public required User CreatedBy { get; set; }

    public ICollection<Product> Products { get; set; } = [];
}