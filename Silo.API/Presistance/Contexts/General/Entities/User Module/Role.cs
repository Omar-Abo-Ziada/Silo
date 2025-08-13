using Microsoft.AspNetCore.Identity;
using Silo.API.Common.Contracts;

namespace Silo.API.Presistance.Contexts.General.Entities.User_Module;

public class Role : IdentityRole<int> , ISoftDeleted
{
    public bool IsDeleted { get; set; } = true;
    public virtual ICollection<User> Users { get; set; } = [];
}