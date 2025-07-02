using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using RentCRL.Application.Users;
using RentCRL.Domain.Users;

namespace RentCRL.Presentation.Users
{
    public static class UserEndpoint
    {
        public const string UserRoute = "/users";

        public static void MapUserEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet(UserRoute, GetUser)
            .RequireAuthorization()
            .WithName("GetUser");
        }

        internal static async Task<IResult> GetUser([FromQuery] string email, IUserService userService)
        {
            var result = await userService.GetUserByEmailAsync(email);

            if (result.IsSuccess)
            {
                Console.WriteLine(result.Value.ToString());
                var newUser= result.Value.ToModel();
                return Results.Ok(newUser);
            }

            if (result.Error == UserErrors.CouldNotFindUserWithEmail)
                return Results.NotFound();

            return Results.Problem(statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}
