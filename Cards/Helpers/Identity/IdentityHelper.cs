using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Core.Utils.WellKnown;

namespace Cards.Helpers.Identity
{
    public static class IdentityHelper
    {
        public static async Task<(SystemUser? user, ActionResult result, bool isInAdminRole)> IsLoggedInUserActive(
            HttpContext context)
        {
            var identity = context.User.Identity as ClaimsIdentity;

            if (identity is null)
                throw new ArgumentNullException(nameof(identity));

            var claims = identity.Claims;

            var claim = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            if (claim is null)
                return (null, new UnauthorizedObjectResult("Claim not found"), false);

            var userId = claim.Value;

            var userManager = context.RequestServices.GetService<UserManager<SystemUser>>();

            var user = await GetUserById(userId, userManager);

            if (user is null)
                return (null, new UnauthorizedObjectResult("User not found"), false);

            var isInAdmin = await userManager.IsInRoleAsync(user, SystemRoles.ADMIN);

            return (user, new OkResult(), isInAdmin);
        }

        public static bool IsUserAdminFromToken(HttpContext context)
        {
            var identity = context.User.Identity as ClaimsIdentity;

            if (identity is null)
                throw new ArgumentNullException(nameof(identity));

            var claims = identity.Claims;

            // User not logged in
            if (!claims.Any())
                return false;

            var claim = claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);

            return claim?.Value == SystemRoles.ADMIN;
        }

        public static async Task<SystemUser?> GetUserById(string userId, UserManager<SystemUser>? userManager)
        {
            if (userManager is null)
                throw new ArgumentNullException(nameof(userManager));

            return await userManager.FindByIdAsync(userId);
        }
    }
}