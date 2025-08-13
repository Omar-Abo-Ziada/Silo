using Silo.API.Common.Contracts;

namespace Silo.API.Presistance.Contexts.General.Entities.Common;

public class BaseModel : ISoftDeleted
{
    public int Id { get; set; }
    public bool IsDeleted { get ; set ; }
}