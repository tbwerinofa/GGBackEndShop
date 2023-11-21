using System.Security.Claims;

namespace ProductWebAPI
{
    public static class Extensions
    {
       
        public static string? ExtractUserId(this IEnumerable<Claim> claims, string claimType)
        {
            var claim = claims.FirstOrDefault(a => a.Type == claimType.ToLower());
            if (claim != null)
                return claim.Value;

            return null;
        }
    }

    public class Utilities
    {
        public enum ClaimTypeEnum
        {
            sid
        }
    }
}
