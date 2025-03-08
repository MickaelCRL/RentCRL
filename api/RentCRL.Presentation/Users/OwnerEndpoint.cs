using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using RentCRL.Application.Users;
using RentCRL.Domain.Users;
using RentCRL.Presentation.Users;

namespace RentCRL.Presentation.Users
{
    public static class OwnerEndpoint
    {
        public const string GetOwnerRoute = "/owners";
        public static void MapOwnerEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost(GetOwnerRoute, ([FromBody] OwnerModel ownerModel, IOwnerService ownerService) =>
            {                
                var newOwner = ownerService.CreateOwnerAsync(ownerModel.Auth0Id, ownerModel.FirstName, ownerModel.LastName, ownerModel.Email, ownerModel.PhoneNumber);

                return newOwner;
            })
            .RequireAuthorization()
            .WithName("Owners");
        }   
    }
}
