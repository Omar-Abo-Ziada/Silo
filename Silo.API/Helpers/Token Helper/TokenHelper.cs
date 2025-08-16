

namespace Silo.API.Common.Helpers.TokenHelper;

public class TokenHelper : ITokenHelper
{
    private readonly IConfiguration _configuration;
    public TokenHelper(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user)
    {

        List<Claim> claims = new();
        foreach(var role in user.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
        }

        claims.Add(new Claim("ID", user.Id.ToString()));
        claims.Add(new Claim(ClaimTypes.Name, user.FullName));

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddHours(1),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.Unicode.GetBytes(_configuration["Jwt:Key"])), SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);

    }
}
