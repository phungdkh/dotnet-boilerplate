using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SampleProject.Application.Common.Constants;
using SampleProject.Application.Services.Authentication.DTO;
using SampleProject.Domain;
using SampleProject.Domain.Entities;

namespace SampleProject.Application.Services.Authentication
{
    public class AppAuthenticationService(AppDbContext appDbContext, IConfiguration configuration) : IAppAuthenticationService
    {
        public async Task<AuthenticateResponse> Authenticate(ApplicationUser user, IList<string> roles, CancellationToken cancellationToken)
        {
            var accessToken = GenerateAccessToken(user, roles);
            var refreshToken = GenerateRefreshToken(user, roles);
            await appDbContext.RefreshTokens.AddAsync(new RefreshToken(user.Id.ToString(), refreshToken), cancellationToken);
            await appDbContext.SaveChangesAsync(cancellationToken);
            return new AuthenticateResponse(accessToken, refreshToken);
        }

        public bool Validate(string refreshToken)
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("Jwt:RefreshKey"))),
                ClockSkew = TimeSpan.Zero
            };

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();

            jwtSecurityTokenHandler.ValidateToken(refreshToken, validationParameters, out var _);
            return true;
        }

        private string GenerateRefreshToken(ApplicationUser user, IList<string> roles)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("Jwt:RefreshKey"));
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaims(user, roles),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = credentials,
            };

            var token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }

        private string GenerateAccessToken(ApplicationUser user, IList<string> roles)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("Jwt:AccessKey"));
            var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaims(user, roles),
                Expires = DateTime.Now.AddMinutes(15),
                SigningCredentials = credentials,
            };

            var token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }

        private static ClaimsIdentity GenerateClaims(ApplicationUser user, IList<string> roles)
        {
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.Name, user.Email));
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Email));

            if (user.CompanyId.HasValue)
                claims.AddClaim(new Claim(AppCustomClaims.CompanyId, user.CompanyId.Value.ToString()));

            foreach (var role in roles)
                claims.AddClaim(new Claim(ClaimTypes.Role, role));

            return claims;
        }
    }
}
