namespace Silo.API.Common.Responses;

public class TokenDTO
{
    public string Token { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public DateTime ExpiresAt { get; set; }
    public IList<string> Roles { get; set; } = [];
}