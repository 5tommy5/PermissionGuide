using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using PermissionApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PermissionApi.Services
{
    public class TokenService
    {
        public TokenModel CreateToken(string userId, PermissionEnum permissions)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Role, ((int)permissions).ToString()),
            };

            var createTime = DateTime.UtcNow;
            var expiresTime = createTime.Add(JwtConfig.Current.Lifetime);

            var token = new JwtSecurityToken(
                issuer: JwtConfig.Current.Issuer,
                audience: JwtConfig.Current.Audience,
                claims: claims,
                expires: expiresTime,
                signingCredentials: new SigningCredentials(
                        JwtConfig.Current.SymmetricSecurityKey,
                        JwtConfig.Current.SigningAlgorithm)
            );

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            var result = new TokenModel()
            {
                AccessToken = jwtToken,
                Expire = expiresTime,
            };

            return result;
        }

        public UserClaims GetUserClaims(string bearer)
        {
            var token = bearer.Replace("Bearer ", "").Replace("bearer ", "");

            var jwt = new JwtSecurityTokenHandler();

            if (jwt.CanReadToken(token))
            {
                IdentityModelEventSource.ShowPII = true;

                SecurityToken validatedToken;
                TokenValidationParameters validationParameters = new TokenValidationParameters();

                validationParameters.ValidateLifetime = false;

                validationParameters.ValidAudience = JwtConfig.Current.Audience;
                validationParameters.ValidIssuer = JwtConfig.Current.Issuer;
                validationParameters.IssuerSigningKey = JwtConfig.Current.SymmetricSecurityKey;

                ClaimsPrincipal principal = jwt.ValidateToken(token, validationParameters, out validatedToken);

                var id = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                var role = (PermissionEnum)int.Parse(principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value ?? "0");

                return new UserClaims()
                {
                    Id = Guid.Parse(id),
                    Permission = role
                };
            }

            throw new ArgumentException("Access token is not valid");
        }
    }
}
