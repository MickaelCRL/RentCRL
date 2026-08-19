using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using RentCRL.Application.Users;
using RentCRL.Domain;
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
        }

        internal static async Task<IResult> CreateOwner([FromBody] OwnerModel ownerModel, IOwnerService ownerService, IValidator<OwnerModel> validator)
        {
            var validationResult = validator.Validate(ownerModel);
            if (!validationResult.IsValid)
                return Results.ValidationProblem(validationResult.ToDictionary());

            var domainAddress = new Address(
                ownerModel.Address.Line1,
                ownerModel.Address.Line2,
                ownerModel.Address.PostalCode,
                ownerModel.Address.City,
                ownerModel.Address.Country
            );

            var result = await ownerService.CreateOwnerAsync(
                ownerModel.Auth0Id,
                ownerModel.FirstName,
                ownerModel.LastName,
                ownerModel.Email,
                ownerModel.PhoneNumber,
                domainAddress
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
    }
}
