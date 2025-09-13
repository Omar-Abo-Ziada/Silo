
namespace Silo.API.Common.Responses;

public enum ErrorType
{
    [Display(Name = "Failure")]
    Failure,

    [Display(Name = "Validation")]
    Validation,

    [Display(Name = "NotFound")]
    NotFound,

    [Display(Name = "Conflict")]
    Conflict,

    [Display(Name = "General")]
    General
}

public sealed record Error(string Code, string Description, ErrorType Type)
{
    public static readonly Error None =
        new("NONE", string.Empty, ErrorType.Failure);

    public static readonly Error NullValue =
        new("NULLVALUE", "The specified result is null.", ErrorType.Failure);

    public static readonly Error General =
        new("GENRAL", "Something went wrong, please try again later.", ErrorType.General);
}
