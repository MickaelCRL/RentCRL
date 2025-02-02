using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCRL.Domain
{
    public interface IOwnerRepository
    {
        void RegisterOwner(Owner owner);

    }
}
