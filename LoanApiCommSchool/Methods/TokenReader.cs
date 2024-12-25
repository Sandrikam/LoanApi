using System;
using System.Linq;
using System.Security.Claims;

namespace LoanApiCommSchool.Methods
{
    public static class TokenReader
    {
        /// Extract the UserId claim from the token.
        public static int? GetUserIdFromToken(ClaimsPrincipal userClaims)
        {
            var userIdClaim = userClaims.Claims.FirstOrDefault(c => c.Type == "UserId");
            return userIdClaim != null ? int.Parse(userIdClaim.Value) : (int?)null;
        }

        /// Extracts the Role claim from the token.
        public static string GetRoleFromToken(ClaimsPrincipal userClaims)
        {
            return userClaims.FindFirst(ClaimTypes.Role)?.Value;
        }
    }
}
