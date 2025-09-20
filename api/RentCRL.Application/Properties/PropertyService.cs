using RentCRL.Domain;
using RentCRL.Domain.Base;
using RentCRL.Domain.Properties;
using RentCRL.Domain.Results;
using System.Net;

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

        public async Task<Result> DeletePropertyByIdAsync(Guid propertyId)
        {
            var property = await _propertyRepository.GetByIdAsync(propertyId);

            if (property != null)
                await _propertyRepository.DeleteAsync(property.Id);

            return Result.Success();
        }

        public async Task<Result<List<Property>>> GetPropertiesByOwnerIdAsync(Guid ownerId)
        {
            var response = await _propertyRepository.GetPropertiesByOwnerIdAsync(ownerId);
            if (response == null || response.Count == 0)
                return PropertyErrors.CouldNotFoundPropertiesByOwnerId;

            return response;
        }
    }
}
