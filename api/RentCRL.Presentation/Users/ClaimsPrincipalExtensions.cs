using RentCRL.Application.Users;
using System.Security.Claims;

namespace RentCRL.Presentation.Users
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetEmail(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.Email);
        }

        public static async Task<bool> IsOwnerEmailMatchingAsync(this ClaimsPrincipal user, Guid ownerId, IOwnerService ownerService)
        {
            var result = await ownerService.GetOwnerByIdAsync(ownerId);
            if (!result.IsSuccess) return false;

            var emailFromOwner = result.Value.Email;
            var emailFromClaimsPrincipal = user.GetEmail();

            return emailFromOwner == emailFromClaimsPrincipal;
        }
    }
}
