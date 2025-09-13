namespace Silo.API.Servies.User;

public class UserStateService : IUserStateService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserStateService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int UserId => _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value is string id ? int.Parse(id) : default;

    public string? UserName => _httpContextAccessor.HttpContext?.User.Identity?.Name;
}