using RentCRL.Domain.Base;

namespace RentCRL.Domain.Properties
{
    public interface IPropertyRepository : IEntityRepository<Property>
    { 
        Task<IEnumerable<Property>> GetPropertiesByOwnerIdAsync(Guid ownerId);
    }
}
