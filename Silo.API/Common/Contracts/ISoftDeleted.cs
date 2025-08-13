namespace Silo.API.Common.Contracts;

public interface ISoftDeleted
{
    public bool IsDeleted { get; set; }
}