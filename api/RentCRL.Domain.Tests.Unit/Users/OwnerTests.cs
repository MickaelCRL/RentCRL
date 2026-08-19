using System;
using AutoFixture;
using AutoFixture.NUnit3;
using RentCRL.Domain.Users;
using RentCRL.Tests.Utils;
using Shouldly;
using NUnit.Framework;

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
            var address = _fixture.Create<Address>();

            var owner = new Owner(id, auth0Id, firstName, lastName, email, phoneNumber, address);

            owner.EntityType.ShouldBe(nameof(Owner));
            owner.Address.ShouldNotBeNull();
        }

        [Test, AutoData]
        public void Constructor_AddressIsNull_ThrowsArgumentNullException
        (
            Guid id,
            string auth0Id,
            string firstName,
            string lastName
        )
        {
            var email = _fixture.CreateEmail();
            var phoneNumber = _fixture.CreatePhoneNumber();

            var exception = Should.Throw<ArgumentNullException>(() =>
                new Owner(id, auth0Id, firstName, lastName, email, phoneNumber, null!)
            );

            exception.ParamName.ShouldBe("address");
        }

        [Test, AutoData]
        public void JsonConstructor_AddressIsNull_ThrowsArgumentNullException
        (
            Guid id,
            string auth0Id,
            string firstName,
            string lastName,
            DateTimeOffset created,
            DateTimeOffset modified
        )
        {
            var email = _fixture.CreateEmail();
            var phoneNumber = _fixture.CreatePhoneNumber();

            var exception = Should.Throw<ArgumentNullException>(() =>
                new Owner(id, auth0Id, firstName, lastName, email, phoneNumber, null!, created, modified)
            );

            exception.ParamName.ShouldBe("address");
        }
    }
}