namespace Silo.API.Servies.User;

public interface IUserStateService
{
    /// <summary>
    /// Gets the current user ID.
    /// </summary>
    int UserId { get; }
    /// <summary>
    /// Gets the current user name.
    /// </summary>
    string? UserName { get; }
}
