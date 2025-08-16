
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

public sealed record Error(HttpStatusCode Code, string Description, ErrorType Type)
{
    public static readonly Error None =
        new(HttpStatusCode.OK, string.Empty, ErrorType.Failure);

    public static readonly Error NullValue =
        new(HttpStatusCode.BadRequest, "The specified result is null.", ErrorType.Failure);

    public static readonly Error General =
        new(HttpStatusCode.InternalServerError, "Something went wrong, please try again later.", ErrorType.General);
}
