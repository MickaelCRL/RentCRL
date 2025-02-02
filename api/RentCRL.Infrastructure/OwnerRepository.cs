using RentCRL.Domain;

namespace RentCRL.Infrastructure
{
    public class OwnerRepository : IOwnerRepository
    {
        public void RegisterOwner(Owner owner)
        {

            Console.WriteLine($"Owner {owner.FirstName} {owner.LastName} enregistré avec succès !");
        }

    }
}
