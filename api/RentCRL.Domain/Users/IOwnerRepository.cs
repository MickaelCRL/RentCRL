using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCRL.Domain.Users
{
    public interface IOwnerRepository
    {
        Task<Owner> AddAsync(Owner owner);
    }
}
