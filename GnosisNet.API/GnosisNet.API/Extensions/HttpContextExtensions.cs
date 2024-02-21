using System.Security.Claims;

namespace GnosisNet.API.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetCurrentUserId(this HttpContext httpContext)
        {
            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            return userIdClaim?.Value;
        }
    }
}
