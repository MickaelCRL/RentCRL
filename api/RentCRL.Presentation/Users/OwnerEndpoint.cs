using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using RentCRL.Application.Users;

namespace RentCRL.Presentation.Users
{
    public static class OwnerEndpoint
    {
        public const string PostOwnerRoute = "/owners";
        public static void MapOwnerEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost(PostOwnerRoute, CreateOwner)
            .RequireAuthorization()
            .WithName("Owners");
        }

        internal static async Task<IResult> CreateOwner( [FromBody] OwnerModel ownerModel, IOwnerService ownerService, IValidator<OwnerModel> validator)
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

            if (result.Error.Code == "OwnerWithEmailAlreadyExists")
                return Results.Conflict();

            return Results.Problem(statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}
