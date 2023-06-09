#nullable disable
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace bahmapi.Services
{
    public class AuthenticatedUser
    {

        private readonly IHttpContextAccessor _accessor;

        public AuthenticatedUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        // public string Email => _accessor.HttpContext.User.Identity.Name;
        public int Id => Convert.ToInt32(_accessor.HttpContext.User.Identity.Name);
        // public string Name => GetClaimsIdentity().FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;

        

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;

        }
    }
}
