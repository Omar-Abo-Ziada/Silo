using System.ComponentModel.DataAnnotations;

namespace Silo.API.Presistance.Contexts.General.Entities.User_Module;

public enum Roles
{
    [Display(Name = "Admin")]
    Admin = 1,

    [Display(Name = "User")]
    User = 2,
}