using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UKG.HCM.Application.Entities;
using UKG.HCM.Application.Services;
using UKG.HCM.WebApi.Endpoints.Login;

namespace UKG.HCM.WebApi.Services;


public interface IJwtTokenGenerator
{
    string Generate(Account account);
}

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtOptions _options;
    private readonly IProvideCurrentDateTime _time;

    public JwtTokenGenerator(
        IOptions<JwtOptions> options,
        IProvideCurrentDateTime time)
    {
        _time = time;
        _options = options.Value;
    }

    public string Generate(Account account)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, account.AccountId.ToString()),
            new("Login", account.Login),
            new(ClaimTypes.GivenName, account.Person.Name.FirstName),
            new(ClaimTypes.Surname, account.Person.Name.LastName),
        };
        
        claims.AddRange(account.Roles.Select(x => new Claim(ClaimTypes.Role, x)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SigningKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: _time.DateTimeOffsetNow().AddHours(24).ToUniversalTime().DateTime,
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}