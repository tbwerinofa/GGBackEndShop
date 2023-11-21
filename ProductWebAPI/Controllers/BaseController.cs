using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ProductWebAPI.Controllers
{

    public abstract class BaseController : ControllerBase
    {

        public string? GetUserId()
        {
            if (User.Claims == null)
            {
                return null;
            }

            return ExtractUserId(User.Claims,ClaimTypes.Sid.ToString());
        }

        private string ExtractUserId(IEnumerable<Claim> claims, string claimType)
        {
            var claim = claims.FirstOrDefault(a => a.Type == claimType);
            if (claim != null)
                return claim.Value;

            return string.Empty;
        }
    }
}
