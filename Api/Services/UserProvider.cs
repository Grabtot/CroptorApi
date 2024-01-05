using Croptor.Application.Common.Interfaces;
using System.Security.Claims;

namespace Croptor.Api.Services
{
    public class UserProvider(IHttpContextAccessor contextAccessor) : IUserProvider
    {
        private readonly ClaimsPrincipal? _user = contextAccessor.HttpContext?.User;

        public Guid? UserId
        {
            get
            {
                string? userIdClaim = _user?.FindFirstValue(ClaimTypes.NameIdentifier);

                if (!string.IsNullOrEmpty(userIdClaim))
                    return Guid.Parse(userIdClaim);
                else
                    return null;
            }
        }

        public string? UserName
        {
            get
            {
                if (_user is null)
                    return null;

                IEnumerable<Claim> claims = _user.Claims;

                string? userNameClaim = claims.FirstOrDefault(c => c.Type == "name")?.Value;
                if (!string.IsNullOrEmpty(userNameClaim))
                    return userNameClaim;
                else
                    return null;
            }
        }

    }
}
