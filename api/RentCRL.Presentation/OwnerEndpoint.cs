using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using RentCRL.Application;
using RentCRL.Domain;
using System.Runtime.InteropServices;

namespace RentCRL.Presentation
{
    public static class OwnerEndpoint
    {
        public const string GetOwnerRoute = "/owners";
        public static void MapOwnerEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost(GetOwnerRoute, ([FromBody] OwnerModel ownerModel, IOwnerService ownerService) =>
            {                
                Console.WriteLine("OwnerModel: " + ownerModel);

                var newOwner = ownerService.CreateOwner(ownerModel.Auth0Id, ownerModel.FirstName, ownerModel.LastName, ownerModel.Email, ownerModel.PhoneNumber);


                return newOwner;

            })
            .RequireAuthorization()
            .WithName("Owners");
        }   
    }
}
