using System.Security.Claims;

namespace CinemaWebAppOriginal.Infrastructure.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        // method that should be able to use in the view for userId retrieval ---- TODO does not work 
        public static string GetUserId(this ClaimsPrincipal userClaimsPrincipal)
            {
                if (userClaimsPrincipal == null)
                {
                    throw new ArgumentNullException(nameof(userClaimsPrincipal));
                }
    
                string ?userIdClaim = userClaimsPrincipal.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? null;

            return userIdClaim;
        }
    }
}
