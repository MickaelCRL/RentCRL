using AutoFixture;
using AutoFixture.NUnit3;
using RentCRL.Domain.Users;
using RentCRL.Tests.Utils;
using Shouldly;

namespace RentCRL.Domain.Tests.Unit.Users
{
    public class OwnerTests
    {
        private Fixture _fixture;

        [SetUp] 
        public void SetUp() 
        {
            _fixture = new Fixture();
        }

        [Test, AutoData] 
        public void Constructor_EntityType_ReturnOwner
        (
            Guid id,
            string auth0Id,
            string firstName,
            string lastName
        ) 
        {
            var email = _fixture.CreateEmail();
            var phoneNumber = _fixture.CreatePhoneNumber();

            var owner = new Owner(id, auth0Id, firstName, lastName, email, phoneNumber);

            owner.EntityType.ShouldBe(nameof(Owner));
        }
    }
}
