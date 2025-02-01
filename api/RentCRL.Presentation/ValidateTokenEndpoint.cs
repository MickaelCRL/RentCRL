

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace RentCRL.Presentation
{
    public static class ValidateTokenEndpoint
    {
        public const string GetValidateTokenRoute = "/users";

        public static void MapValidateTokenEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet(GetValidateTokenRoute, (HttpContext context) =>
            {
                if (context.User.Identity?.IsAuthenticated == true)
                {
                    var userClaims = context.User.Claims.Select(claim => new
                    {
                        claim.Type,
                        claim.Value
                    }).ToList();
                    var response = new
                    {
                        message = "token is valid",
                        userClaims,
                    };
                    context.Response.ContentType = "application/json";
                    return context.Response.WriteAsJsonAsync(response);
                }
                context.Response.StatusCode = 401;
                var errorResponse = new { message = "Unauthorized" };
                return context.Response.WriteAsJsonAsync(errorResponse);
            })
            .RequireAuthorization()
            .WithName("Users");
        }


    }
}
