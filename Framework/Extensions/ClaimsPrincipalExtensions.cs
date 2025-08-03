using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Framework.Enums;
using Framework.Exceptions;
using Framework.Models;

namespace Framework.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static UserSession GetUserSession(this ClaimsPrincipal user)
        {
            var guid = user.GetGuid();
            var role = user.GetRole();
            var jti = user.GetJti();

            if (!guid.HasValue || !role.HasValue)
            {
                throw new QlUnauthorizedException("Invalid user session");
            }

            /*var sessionToken = await GetCachedSession(guid.Value);
            if (sessionToken == null)
            {
                return null;
            }

            if (jti != sessionToken.Jti)
            {
                return null;
            }*/

            return new UserSession() { Guid = guid.Value, SystemRole = role.Value };
        }

        private static Guid? GetGuid(this ClaimsPrincipal user)
        {
            if (user == null)
            {
                return null;
            }

            var guidClaim = user.Claims.FirstOrDefault(c => c.Type == "Guid");
            if (guidClaim != null && Guid.TryParse(guidClaim.Value, out var userGuid))
            {
                return userGuid;
            }
            return null;
        }

        private static string GetJti(this ClaimsPrincipal user)
        {
            if (user == null)
            {
                return string.Empty;
            }

            var jtiClaim = user.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);
            if (jtiClaim == null)
            {
                return string.Empty;
            }

            return jtiClaim.Value;
        }

        private static SystemRole? GetRole(this ClaimsPrincipal user)
        {
            if (user == null)
            {
                return null;
            }

            var roleClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim != null)
            {
                if (Enum.TryParse<SystemRole>(roleClaim.Value, true, out var role))
                {
                    return role;
                }
            }
            return null;
        }
    }
}
