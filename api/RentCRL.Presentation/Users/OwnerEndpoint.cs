using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using RentCRL.Application.Users;
using RentCRL.Domain.Users;

namespace RentCRL.Presentation.Users
{
    public static class OwnerEndpoint
    {
        public const string OwnerRoute = "/owners";

        public static void MapOwnerEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost(OwnerRoute, CreateOwner)
            .RequireAuthorization()
            .WithName("CreateOwner");

            app.MapGet(OwnerRoute, GetOwner)
                .RequireAuthorization()
                .WithName("GetOwner");
        }

        internal static async Task<IResult> CreateOwner([FromBody] OwnerModel ownerModel, IOwnerService ownerService, IValidator<OwnerModel> validator)
        {
            var validationResult = validator.Validate(ownerModel);
            if (!validationResult.IsValid)
                return Results.ValidationProblem(validationResult.ToDictionary());

            var result = await ownerService.CreateOwnerAsync(
                ownerModel.Auth0Id,
                ownerModel.FirstName,
                ownerModel.LastName,
                ownerModel.Email,
                ownerModel.PhoneNumber
            );

            if (result.IsSuccess)
            {
                var newOwner = result.Value.ToModel();
                return Results.Ok(newOwner);
            }

            if (result.Error == UserErrors.EmailAlreadyExists)
                return Results.Conflict();

            return Results.Problem(statusCode: StatusCodes.Status500InternalServerError);
        }

        internal static async Task<IResult> GetOwner([FromQuery] string email, IOwnerService ownerService)
        {
            var result = await ownerService.GetOwnerByEmailAsync(email);

            if (result.IsSuccess)
            {
               Console.WriteLine(result.Value.ToString());
                var newOwner = result.Value.ToModel();
                return Results.Ok(newOwner);
            }

            if (result.Error == UserErrors.CouldNotFindUserWithEmail)
                return Results.NotFound();

            return Results.Problem(statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}
