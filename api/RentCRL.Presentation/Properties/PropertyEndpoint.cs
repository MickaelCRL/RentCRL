using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using RentCRL.Application.Properties;
using RentCRL.Application.Users;
using RentCRL.Domain.Properties;
using RentCRL.Presentation.Addresses;
using RentCRL.Presentation.Users;
using System.Security.Claims;

namespace RentCRL.Presentation.Properties
{
    public static class PropertyEndpoint
    {
        public const string PropertyRoute = "/owners/{ownerId:guid}/properties";
        public static void MapPropertyEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost(PropertyRoute, CreateProperty)
            .RequireAuthorization()
            .WithName("CreateProperty");

            app.MapGet(PropertyRoute, GetProperties)
            .RequireAuthorization()
            .WithName("GetProperties");
        }

        internal static async Task<IResult> GetProperties(Guid ownerId, IPropertyService propertyService)
        {
            var result = await propertyService.GetPropertiesByOwnerIdAsync(ownerId);

            if (result.IsSuccess)
            {
                Console.WriteLine(result.Value.ToString());
                var properties = result.Value;
                return Results.Ok(properties);
            }

            if (result.Error == PropertyErrors.CouldNotFoundPropertiesByOwnerId)
                return Results.NotFound();
            return Results.Problem(statusCode: StatusCodes.Status500InternalServerError);
        }

        internal static async Task<IResult> CreateProperty(
            [FromBody] PropertyModel propertyModel,
            Guid ownerId,
            IPropertyService propertyService,
            IOwnerService ownerService,
            IValidator<PropertyModel> validator,
            ClaimsPrincipal user
        )
        {
            var validationResult = validator.Validate(propertyModel);
            if (!validationResult.IsValid)
                return Results.ValidationProblem(validationResult.ToDictionary());

            Console.WriteLine("owner id : " + ownerId);
            var ownerResult = ownerService.GetOwnerByIdAsync(ownerId);
            var email = user.GetEmail();

            if (ownerResult.Result.Value.Email != email)
                return Results.Unauthorized();

            var result = await propertyService.CreatePropertyAsync(
                propertyModel.Name,
                propertyModel.Surface,
                propertyModel.Status,
                propertyModel.Address.ToAddress(),
                ownerId
            );

            if (result.IsSuccess)
            {
                var newProperty = result.Value.ToModel();
                return Results.Ok(newProperty);
            }

            return Results.Problem(statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}
