using RentCRL.Domain.Base;

namespace RentCRL.Domain.Properties
{
    public interface IPropertyRepository : IEntityRepository<Property>
    { 
        Task<List<Property>> GetPropertiesByOwnerIdAsync(Guid ownerId);
        Task<Property> UpdatePropertyAsync(Guid propertyId, string name, decimal surface, string status, Address address);
    }
}
