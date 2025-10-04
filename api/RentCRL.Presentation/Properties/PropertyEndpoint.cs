using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using RentCRL.Application.Properties;
using RentCRL.Application.Users;
using RentCRL.Domain.Properties;
using RentCRL.Domain.Users;
using RentCRL.Presentation.Addresses;
using RentCRL.Presentation.Users;
using System.Security.Claims;

namespace RentCRL.Presentation.Properties
{
    public static class PropertyEndpoint
    {
        public const string PropertyRoute = "/owners/{ownerId:guid}/properties";
        public const string DeletePropertyRoute = "/owners/{ownerId:guid}/properties/{propertyId:guid}";

        public static void MapPropertyEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost(PropertyRoute, CreateProperty)
                .RequireAuthorization()
                .WithName("CreateProperty");

            app.MapGet(PropertyRoute, GetProperties)
                .RequireAuthorization()
                .WithName("GetProperties");

            app.MapDelete(PropertyRoute, DeleteProperty)
                .RequireAuthorization()
                .WithName("DeleteProperty");
        }

        internal static async Task<IResult> DeleteProperty(
            Guid ownerId,
            Guid propertyId,
            IOwnerService ownerService,
            IPropertyService propertyService,
            ClaimsPrincipal user
        )
        {
            var IsOwnerEmailValid = await IsOwnerEmailMatchingClaimsPrincipal(ownerId, ownerService, user);
            if (!IsOwnerEmailValid)
                return Results.Unauthorized();

            var result = await propertyService.DeletePropertyByIdAsync(propertyId);

            if (result.IsSuccess)
                return Results.NoContent();

            return Results.Problem(statusCode: StatusCodes.Status500InternalServerError);
        }

        internal static async Task<IResult> GetProperties(
            Guid ownerId,
            IOwnerService ownerService,
            IPropertyService propertyService,
            ClaimsPrincipal user
        )
        {
            var IsOwnerEmailValid = await IsOwnerEmailMatchingClaimsPrincipal(ownerId, ownerService, user);
            if (!IsOwnerEmailValid)
                return Results.Unauthorized();

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

            var IsOwnerEmailValid = await IsOwnerEmailMatchingClaimsPrincipal(ownerId, ownerService, user);
            if (!IsOwnerEmailValid)
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

        private async static Task<bool> IsOwnerEmailMatchingClaimsPrincipal(Guid ownerId, IOwnerService ownerService, ClaimsPrincipal user)
        {
            var result = await ownerService.GetOwnerByIdAsync(ownerId);
            var emailFromOwner = result.Value.Email; 
            var emailFromClaimsPrincipal = user.GetEmail();

            if (emailFromOwner != emailFromClaimsPrincipal)
                return false;

            return true;
        }
    }
}
