using RentCRL.Domain.Base;

namespace RentCRL.Infrastructure.Base
{
    public class GuidProvider : IGuidProvider
    {
        public Guid NewGuid()
        {
            return Guid.NewGuid();
        }
    }
}
