using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using RentCRL.Application.Contracts;
using RentCRL.Application.Users;
using RentCRL.Domain.Contracts;
using RentCRL.Presentation.Users;
using System.Security.Claims;

namespace RentCRL.Presentation.Contracts
{
    public static class ContractEndpoint
    {
        public const string ContractsRoute = "/owners/{ownerId:guid}/contracts";
        public const string ContractRoute = "/owners/{ownerId:guid}/contracts/{contractId:guid}";

        public static void MapContractEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost(ContractsRoute, CreateContract)
                .RequireAuthorization()
                .WithName("CreateContract");

            app.MapGet(ContractsRoute, GetContracts)
                .RequireAuthorization()
                .WithName("GetContracts");

            app.MapDelete(ContractRoute, DeleteContract)
                .RequireAuthorization()
                .WithName("DeleteContract");

            app.MapGet(ContractRoute, GetContract)
               .RequireAuthorization()
               .WithName("GetContract");
        }

        internal static async Task<IResult> GetContract(
            Guid ownerId,
            Guid contractId,
            IOwnerService ownerService,
            IContractService contractService,
            ClaimsPrincipal user
        )
        {
            var isOwnerEmailValid = await user.IsOwnerEmailMatchingAsync(ownerId, ownerService);
            if (!isOwnerEmailValid)
                return Results.Unauthorized();

            var result = await contractService.GetContractByIdAsync(contractId);

            if (result.IsSuccess)
            {
                var contract = result.Value.ToModel();
                return Results.Ok(contract);
            }

            if (result.Error == ContractErrors.CouldNotFoundContractById)
                return Results.NotFound();

            return Results.Problem(statusCode: StatusCodes.Status500InternalServerError);
        }

        internal static async Task<IResult> DeleteContract(
            Guid ownerId,
            Guid contractId,
            IOwnerService ownerService,
            IContractService contractService,
            ClaimsPrincipal user
        )
        {
            var isOwnerEmailValid = await user.IsOwnerEmailMatchingAsync(ownerId, ownerService);
            if (!isOwnerEmailValid)
                return Results.Unauthorized();

            var result = await contractService.DeleteContractByIdAsync(contractId);

            if (result.IsSuccess)
                return Results.NoContent();

            return Results.Problem(statusCode: StatusCodes.Status500InternalServerError);
        }

        internal static async Task<IResult> GetContracts(
            Guid ownerId,
            IOwnerService ownerService,
            IContractService contractService,
            ClaimsPrincipal user
        )
        {
            var isOwnerEmailValid = await user.IsOwnerEmailMatchingAsync(ownerId, ownerService);
            if (!isOwnerEmailValid)
                return Results.Unauthorized();

            var result = await contractService.GetContractsByOwnerIdAsync(ownerId);

            if (result.IsSuccess)
            {
                var contracts = result.Value.Select(c => c.ToModel()).ToList();
                return Results.Ok(contracts);
            }

            if (result.Error == ContractErrors.CouldNotFoundContractsByOwnerId)
                return Results.NotFound();

            return Results.Problem(statusCode: StatusCodes.Status500InternalServerError);
        }

        internal static async Task<IResult> CreateContract(
            [FromBody] ContractModel contractModel,
            Guid ownerId,
            IContractService contractService,
            IOwnerService ownerService,
            IValidator<ContractModel> validator,
            ClaimsPrincipal user
        )
        {
            var validationResult = validator.Validate(contractModel);
            if (!validationResult.IsValid)
                return Results.ValidationProblem(validationResult.ToDictionary());

            var isOwnerEmailValid = await user.IsOwnerEmailMatchingAsync(ownerId, ownerService);
            if (!isOwnerEmailValid)
                return Results.Unauthorized();

            var result = await contractService.CreateContractAsync(
                ownerId,
                contractModel.PropertyId,
                contractModel.TenantId,
                contractModel.TenantEmail,
                contractModel.Rent,
                contractModel.Deposit,
                contractModel.FamilyAllowanceFundAmount,
                contractModel.StartDate,
                contractModel.EndDate,
                contractModel.Note
            );

            if (result.IsSuccess)
            {
                var newContract = result.Value.ToModel();
                return Results.Ok(newContract);
            }

            return Results.Problem(statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}