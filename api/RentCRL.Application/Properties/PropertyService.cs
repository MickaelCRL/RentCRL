using RentCRL.Domain;
using RentCRL.Domain.Base;
using RentCRL.Domain.Properties;
using RentCRL.Domain.Results;

namespace RentCRL.Application.Properties
{
    public class PropertyService : IPropertyService
    {
        private readonly IGuidProvider _guidProvider;
        private readonly IPropertyRepository _propertyRepository;
        
        public PropertyService(IGuidProvider guidProvider, IPropertyRepository propertyRepository)
        {
            _guidProvider = guidProvider;
            _propertyRepository = propertyRepository;
        }

        public async Task<Result<Property>> CreatePropertyAsync(string name, decimal surface, string status, Address address, Guid ownerId)
        {
            var property = new Property(_guidProvider.NewGuid(), name, surface, status, address, ownerId);
            return await _propertyRepository.AddAsync(property);
        }

        public async Task<Result<List<Property>>> GetPropertiesByOwnerIdAsync(Guid ownerId)
        {
            var response = await _propertyRepository.GetPropertiesByOwnerIdAsync(ownerId);
            if (response.Count() == 0)
                return Result.Failure<List<Property>>(PropertyErrors.CouldNotFoundPropertiesByOwnerId);

            return Result.Success(response);
        }
    }
}
