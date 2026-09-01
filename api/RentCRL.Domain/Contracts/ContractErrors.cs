using RentCRL.Domain.Results;

namespace RentCRL.Domain.Contracts
{
    public class ContractErrors
    {
        public static readonly Error CouldNotFoundContractsByOwnerId =
            new("CouldNotFoundContractsByOwnerId", "Could not find contracts by ownerId");

        public static readonly Error CouldNotFoundContractById =
            new("CouldNotFoundContractById", "Could not find contract by id");
    }
}