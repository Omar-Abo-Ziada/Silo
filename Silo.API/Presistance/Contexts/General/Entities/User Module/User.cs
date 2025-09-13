namespace Silo.API.Presistance.Contexts.General.Entities.User_Module;

public class User : IdentityUser<int>, ISoftDeleted
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";

    public bool IsDeleted { get; set; } = true;

    public virtual ICollection<Role> Roles { get; set; } = [];
}