namespace Silo.API.Presistance.Contexts.General.Entities.Common;

public class BaseModel : ISoftDeleted
{
    [Key]
    public int Id { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? UpdatedOn { get; set; }
    public int? UpdatedBy { get; set; }

    public int? CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }

    public void delete()
    {
        IsDeleted = true;
    }
}