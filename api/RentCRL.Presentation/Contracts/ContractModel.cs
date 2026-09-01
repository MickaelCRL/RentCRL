using System;

namespace RentCRL.Presentation.Contracts
{
    public record ContractModel
    (
        Guid Id,
        Guid OwnerId,
        Guid PropertyId,
        Guid? TenantId,
        string TenantEmail,
        decimal Rent,
        decimal Deposit,
        decimal FamilyAllowanceFundAmount,
        DateTimeOffset StartDate,
        DateTimeOffset? EndDate,
        string Note
    )
    { }
}