using RentCRL.Domain.Base;

namespace RentCRL.Domain.Properties
{
    public interface IPropertyRepository : IEntityRepository<Property>
    { 
        Task<List<Property>> GetPropertiesByOwnerIdAsync(Guid ownerId);
    }
}
