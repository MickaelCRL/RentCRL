using RentCRL.Domain;
using RentCRL.Domain.Properties;
using RentCRL.Domain.Results;

namespace RentCRL.Application.Properties
{
    public interface IPropertyService
    {
        Task<Result<Property>> CreatePropertyAsync(string name, decimal surface, string status, Address address, Guid ownerId);

        Task<Result<List<Property>>> GetPropertiesByOwnerIdAsync(Guid ownerId);
    }

}
