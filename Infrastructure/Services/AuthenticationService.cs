using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Domain.DTOs;
using Domain.Interfaces;
using Infrastructure.Database;
using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IContext _context;
    private readonly IDateTimeService _dateTimeService;

    private const string Issuer = "";
    private const string Audience = "";
    private const string Secret = "dfghjjhgfdsfghjhgfdsdfghjhgfdsdfghjhgfdsasdfghjhgfdsdfghjhgfdsdfghjhgfdsdfg";

    public AuthenticationService(IContext context, IDateTimeService dateTimeService)
    {
        _context = context;
        _dateTimeService = dateTimeService;
    }

    public async Task<LoginResponse?> GenerateJwtAsync(int userId, CancellationToken cancellationToken)
    {
        var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == userId, cancellationToken);
        if (user is null) return null;

        var jwt = GenerateJwtToken(user);
        var refreshToken = GenerateRefreshToken();
        return new LoginResponse(jwt, refreshToken);
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Email, user.Email),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new (JwtRegisteredClaimNames.Iat, _dateTimeService.Now.ToUnixTimeSeconds().ToString()),
            new (JwtRegisteredClaimNames.Nbf, _dateTimeService.Now.ToUnixTimeSeconds().ToString()),
            new (JwtRegisteredClaimNames.Sub, user.Id.ToString())
        };

        var claimsIdentity = new ClaimsIdentity(claims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            Expires = _dateTimeService.Now.AddMinutes(60).DateTime,
            Issuer = Issuer,
            Audience = Audience,
            SigningCredentials = signingCredentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtString = tokenHandler.WriteToken(token);
        return jwtString;
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}