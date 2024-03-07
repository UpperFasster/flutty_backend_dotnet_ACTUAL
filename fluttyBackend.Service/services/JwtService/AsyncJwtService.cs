using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace fluttyBackend.Service.services.JwtService
{
    public class AsyncJwtService : IAsyncJwtService
    {
        private readonly string secretKey;
        private readonly string issuer;
        private readonly string audience;
        private readonly TimeSpan expiration;

        public AsyncJwtService(string secretKey, string issuer, string audience, TimeSpan expiration)
        {
            if (secretKey.Length < 16)
            {
                throw new ArgumentException("Secret key must be at least 16 characters for HS256.");
            }

            this.secretKey = secretKey;
            this.issuer = issuer;
            this.audience = audience;
            this.expiration = expiration;
        }

        public async Task<string> GenerateTokenAsync(string userId)
        {
            var claims = new List<Claim>
            {
                new(JwtClaimNames.UserId, userId),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var currentUtcTime = DateTime.UtcNow;

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: currentUtcTime,
                expires: currentUtcTime.Add(expiration),
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);

            return await Task.FromResult(tokenString);
        }

        public Task<bool> ValidateTokenAsync(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secretKey);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.FromSeconds(10)
                }, out _);

                return Task.FromResult(true);
            }
            catch (SecurityTokenException)
            {
                return Task.FromResult(false);
            }
        }

        public Task<Guid> GetUserIdFromTokenAsync(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secretKey);

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                }, out _);

                var userIdClaim = principal.FindFirst(JwtClaimNames.UserId);

                if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var userId))
                {
                    return Task.FromResult<Guid>(userId);
                }

                return null;
            }
            catch (SecurityTokenException)
            {
                return null;
            }
        }
    }
}