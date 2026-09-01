using RentCRL.Domain.Contracts;

namespace RentCRL.Presentation.Contracts
{
    public static class ContractExtensions
    {
        public static ContractModel ToModel(this Contract contract)
        {
            return new ContractModel(
                contract.Id,
                contract.OwnerId,
                contract.PropertyId,
                contract.TenantId,
                contract.TenantEmail,
                contract.Rent,
                contract.Deposit,
                contract.FamilyAllowanceFundAmount,
                contract.StartDate,
                contract.EndDate,
                contract.Note
            );
        }
    }
}