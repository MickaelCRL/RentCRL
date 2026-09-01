using AutoFixture.NUnit3;
using RentCRL.Domain.Contracts;
using Shouldly;
using System;

namespace RentCRL.Domain.Tests.Unit.Contracts
{
    [TestFixture]
    public class ContractTests
    {
        #region Constructor

        [Test, AutoData]
        public void Constructor_ValidArgument_CreateContract
        (
            Guid id,
            Guid ownerId,
            Guid propertyId,
            Guid? tenantId,
            string tenantEmail,
            decimal rent,
            decimal deposit,
            decimal familyAllowanceFundAmount,
            string note
        )
        {
            // Arrange
            rent = Math.Abs(rent) + 1;
            deposit = Math.Abs(deposit);
            familyAllowanceFundAmount = Math.Abs(familyAllowanceFundAmount);
            var startDate = DateTimeOffset.UtcNow;
            var endDate = startDate.AddYears(1);

            // Act
            var contract = new Contract(id, ownerId, propertyId, tenantId, tenantEmail, rent, deposit, familyAllowanceFundAmount, startDate, endDate, note);

            // Assert
            contract.ShouldNotBeNull();
            contract.OwnerId.ShouldBe(ownerId);
            contract.PropertyId.ShouldBe(propertyId);
            contract.TenantId.ShouldBe(tenantId);
            contract.TenantEmail.ShouldBe(tenantEmail);
            contract.Rent.ShouldBe(rent);
            contract.Deposit.ShouldBe(deposit);
            contract.FamilyAllowanceFundAmount.ShouldBe(familyAllowanceFundAmount);
            contract.StartDate.ShouldBe(startDate);
            contract.EndDate.ShouldBe(endDate);
            contract.Note.ShouldBe(note);
            contract.EntityType.ShouldBe(nameof(Contract));
        }

        [Test, AutoData]
        public void Constructor_EmptyOwnerId_ThrowArgumentException
        (
            Guid id,
            Guid propertyId,
            Guid? tenantId,
            string tenantEmail,
            decimal rent,
            decimal deposit,
            decimal familyAllowanceFundAmount,
            DateTimeOffset startDate,
            DateTimeOffset? endDate,
            string note
        )
        {
            Guid ownerId = Guid.Empty;

            var action = () =>
            {
                var contract = new Contract(id, ownerId, propertyId, tenantId, tenantEmail, rent, deposit, familyAllowanceFundAmount, startDate, endDate, note);
            };

            action.ShouldThrow<ArgumentException>();
        }

        [Test, AutoData]
        public void Constructor_EmptyPropertyId_ThrowArgumentException
        (
            Guid id,
            Guid ownerId,
            Guid? tenantId,
            string tenantEmail,
            decimal rent,
            decimal deposit,
            decimal familyAllowanceFundAmount,
            DateTimeOffset startDate,
            DateTimeOffset? endDate,
            string note
        )
        {
            Guid propertyId = Guid.Empty;

            var action = () =>
            {
                var contract = new Contract(id, ownerId, propertyId, tenantId, tenantEmail, rent, deposit, familyAllowanceFundAmount, startDate, endDate, note);
            };

            action.ShouldThrow<ArgumentException>();
        }

        [Test, AutoData]
        public void Constructor_NullOrEmptyTenantEmail_ThrowArgumentException
        (
            Guid id,
            Guid ownerId,
            Guid propertyId,
            Guid? tenantId,
            decimal rent,
            decimal deposit,
            decimal familyAllowanceFundAmount,
            DateTimeOffset startDate,
            DateTimeOffset? endDate,
            string note
        )
        {
            string tenantEmail = string.Empty;

            var action = () =>
            {
                var contract = new Contract(id, ownerId, propertyId, tenantId, tenantEmail, rent, deposit, familyAllowanceFundAmount, startDate, endDate, note);
            };

            action.ShouldThrow<ArgumentException>();
        }

        [Test, AutoData]
        public void Constructor_NegativeOrZeroRent_ThrowArgumentException
        (
            Guid id,
            Guid ownerId,
            Guid propertyId,
            Guid? tenantId,
            string tenantEmail,
            decimal deposit,
            decimal familyAllowanceFundAmount,
            DateTimeOffset startDate,
            DateTimeOffset? endDate,
            string note
        )
        {
            decimal rent = 0;

            var action = () =>
            {
                var contract = new Contract(id, ownerId, propertyId, tenantId, tenantEmail, rent, deposit, familyAllowanceFundAmount, startDate, endDate, note);
            };

            action.ShouldThrow<ArgumentException>();
        }

        [Test, AutoData]
        public void Constructor_NegativeDeposit_ThrowArgumentException
        (
            Guid id,
            Guid ownerId,
            Guid propertyId,
            Guid? tenantId,
            string tenantEmail,
            decimal rent,
            decimal familyAllowanceFundAmount,
            DateTimeOffset startDate,
            DateTimeOffset? endDate,
            string note
        )
        {
            decimal deposit = -100;

            var action = () =>
            {
                var contract = new Contract(id, ownerId, propertyId, tenantId, tenantEmail, rent, deposit, familyAllowanceFundAmount, startDate, endDate, note);
            };

            action.ShouldThrow<ArgumentException>();
        }

        [Test, AutoData]
        public void Constructor_DefaultStartDate_ThrowArgumentException
        (
            Guid id,
            Guid ownerId,
            Guid propertyId,
            Guid? tenantId,
            string tenantEmail,
            decimal rent,
            decimal deposit,
            decimal familyAllowanceFundAmount,
            DateTimeOffset? endDate,
            string note
        )
        {
            DateTimeOffset startDate = default;

            var action = () =>
            {
                var contract = new Contract(id, ownerId, propertyId, tenantId, tenantEmail, Math.Abs(rent) + 1, Math.Abs(deposit), Math.Abs(familyAllowanceFundAmount), startDate, endDate, note);
            };

            action.ShouldThrow<ArgumentException>();
        }

        [Test, AutoData]
        public void Constructor_EndDateBeforeStartDate_ThrowArgumentException
        (
            Guid id,
            Guid ownerId,
            Guid propertyId,
            Guid? tenantId,
            string tenantEmail,
            decimal rent,
            decimal deposit,
            decimal familyAllowanceFundAmount,
            string note
        )
        {
            DateTimeOffset startDate = DateTimeOffset.UtcNow;
            DateTimeOffset endDate = startDate.AddDays(-1);

            var action = () =>
            {
                var contract = new Contract(id, ownerId, propertyId, tenantId, tenantEmail, Math.Abs(rent) + 1, Math.Abs(deposit), Math.Abs(familyAllowanceFundAmount), startDate, endDate, note);
            };

            action.ShouldThrow<ArgumentException>();
        }

        #endregion
    }
}