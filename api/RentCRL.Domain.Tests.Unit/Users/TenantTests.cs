using AutoFixture;
using AutoFixture.NUnit3;
using RentCRL.Domain.Users;
using RentCRL.Tests.Utils;
using Shouldly;

namespace RentCRL.Domain.Tests.Unit.Users
{
    public class TenantTests
    {
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
        }

        [Test, AutoData]
        public void Constructor_EntityType_ReturnTenant
        (
            Guid id,
            string auth0Id,
            string firstName,
            string lastName
        )
        {
            var email = _fixture.CreateEmail();
            var phoneNumber = _fixture.CreatePhoneNumber();

            var tenant = new Tenant(id, auth0Id, firstName, lastName, email, phoneNumber);

            tenant.EntityType.ShouldBe(nameof(Tenant));
        }
    }
}
