using RentCRL.Domain;
using RentCRL.Domain.Properties;
using RentCRL.Domain.Results;
using RentCRL.Domain.Users;

namespace RentCRL.Application.Properties
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        
        public PropertyService(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<Result<Property>> CreatePropertyAsync(string name, decimal surface, string status, Address address, Guid ownerId)
        {
            var id = Guid.NewGuid();
            var property = new Property(id, name, surface, status, address, ownerId);
            return await _propertyRepository.AddAsync(property);
        }

        public async Task<Result<IEnumerable<Property>>> GetPropertiesByOwnerIdAsync(Guid ownerId)
        {
            var response = await _propertyRepository.GetPropertiesByOwnerIdAsync(ownerId);
            if (response.Count() == 0)
                return Result.Failure<IEnumerable<Property>>(PropertyErrors.CouldNotFoundPropertiesByOwnerId);

            return Result.Success<IEnumerable<Property>>(response.ToList());
        }
    }
}
