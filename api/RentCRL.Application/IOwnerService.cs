using RentCRL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCRL.Application
{
    public interface IOwnerService
    {
        Task<Owner> CreateOwnerAsync(string auth0Id, string firstName, string lastName, string email, string phoneNumber);
    }
}
