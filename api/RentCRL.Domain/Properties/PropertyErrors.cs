using RentCRL.Domain.Results;

namespace RentCRL.Domain.Properties
{
    public class PropertyErrors
    {
        public static readonly Error CouldNotFoundPropertiesByOwnerId = 
            new("CouldNotFoundPropertiesByOwnerId", "Could not found properties by ownerId");

        public static readonly Error CouldNotFoundPropertyById =
            new("CouldNotFoundPropertyById", "Could not find property by id");
    }
}
