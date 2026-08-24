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
        public const string PropertiesRoute = "/owners/{ownerId:guid}/properties";
        public const string PropertyRoute = "/owners/{ownerId:guid}/properties/{propertyId:guid}";

        public static void MapPropertyEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost(PropertiesRoute, CreateProperty)
                .RequireAuthorization()
                .WithName("CreateProperty");

            app.MapGet(PropertiesRoute, GetProperties)
                .RequireAuthorization()
                .WithName("GetProperties");

            app.MapDelete(PropertyRoute, DeleteProperty)
                .RequireAuthorization()
                .WithName("DeleteProperty");

            app.MapGet(PropertyRoute, GetProperty)
               .RequireAuthorization()
               .WithName("GetProperty");

            app.MapPatch(PropertyRoute, PatchProperty)
               .RequireAuthorization()
               .WithName("PatchProperty");
        }

        internal static async Task<IResult> PatchProperty(
           Guid ownerId,
           PropertyModel propertyModel,
           IOwnerService ownerService,
           IPropertyService propertyService,
           IValidator<PropertyModel> validator,
           ClaimsPrincipal user
        )
        {
            var isOwnerEmailValid = await user.IsOwnerEmailMatchingAsync(ownerId, ownerService);
            if (!isOwnerEmailValid)
                return Results.Unauthorized();

            var validationResult = validator.Validate(propertyModel);
            if (!validationResult.IsValid)
                return Results.ValidationProblem(validationResult.ToDictionary());

            var result = await propertyService.UpdatePropertyAsync(
                propertyModel.Id,
                propertyModel.Name,
                propertyModel.Surface,
                propertyModel.Status,
                propertyModel.Address.ToAddress()
            );

            if (result.IsSuccess)
            {
                var property = result.Value.ToModel();
                return Results.Ok(property);
            }

            if (result.Error == PropertyErrors.CouldNotFoundPropertyById)
                return Results.NotFound();

            return Results.Problem(statusCode: StatusCodes.Status500InternalServerError);
        }

        internal static async Task<IResult> GetProperty(
            Guid ownerId,
            Guid propertyId,
            IOwnerService ownerService,
            IPropertyService propertyService,
            ClaimsPrincipal user
        )
        {
            var IsOwnerEmailValid = await user.IsOwnerEmailMatchingAsync(ownerId, ownerService);
            if (!IsOwnerEmailValid)
                return Results.Unauthorized();

            var result = await propertyService.GetPropertyByIdAsync(propertyId);

            if (result.IsSuccess)
            {
                var property = result.Value;
                return Results.Ok(property);
            }

            if (result.Error == PropertyErrors.CouldNotFoundPropertyById)
                return Results.NotFound();

            return Results.Problem(statusCode: StatusCodes.Status500InternalServerError);
        }

        internal static async Task<IResult> DeleteProperty(
            Guid ownerId,
            Guid propertyId,
            IOwnerService ownerService,
            IPropertyService propertyService,
            ClaimsPrincipal user
        )
        {
            var IsOwnerEmailValid = await user.IsOwnerEmailMatchingAsync(ownerId, ownerService);
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
            var IsOwnerEmailValid = await user.IsOwnerEmailMatchingAsync(ownerId, ownerService);
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

            var IsOwnerEmailValid = await user.IsOwnerEmailMatchingAsync(ownerId, ownerService);
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
    }
}
