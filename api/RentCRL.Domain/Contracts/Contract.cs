using Newtonsoft.Json;
using RentCRL.Domain.Base;

namespace RentCRL.Domain.Contracts
{
    public class Contract : Entity
    {
        public Guid OwnerId { get; private set; }
        public Guid PropertyId { get; private set; }
        public Guid? TenantId { get; private set; }
        public string TenantEmail { get; private set; }
        public decimal Rent { get; private set; }
        public decimal Deposit { get; private set; }
        public decimal FamilyAllowanceFundAmount { get; private set; }
        public DateTimeOffset StartDate { get; private set; }
        public DateTimeOffset? EndDate { get; private set; }
        public string Note { get; private set; }

        public Contract(
            Guid id,
            Guid ownerId,
            Guid propertyId,
            Guid? tenantId,
            string tenantEmail,
            decimal rent,
            decimal deposit,
            decimal familyAllowanceFundAmount,
            DateTimeOffset startDate,
            DateTimeOffset? endDate,
            string note
        ) : base(id, nameof(Contract))
        {
            InitializeProperties(ownerId, propertyId, tenantId, tenantEmail, rent, deposit, familyAllowanceFundAmount, startDate, endDate, note);
        }

        [JsonConstructor]
        public Contract(
            Guid id,
            Guid ownerId,
            Guid propertyId,
            Guid? tenantId,
            string tenantEmail,
            decimal rent,
            decimal deposit,
            decimal familyAllowanceFundAmount,
            DateTimeOffset startDate,
            DateTimeOffset? endDate,
            string? note,
            DateTimeOffset? created,
            DateTimeOffset? modified
        ) : base(id, nameof(Contract), created, modified)
        {
            InitializeProperties(ownerId, propertyId, tenantId, tenantEmail, rent, deposit, familyAllowanceFundAmount, startDate, endDate, note);
        }

        private void InitializeProperties(
            Guid ownerId,
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
            if (ownerId == Guid.Empty)
                throw new ArgumentException("OwnerId cannot be empty.", nameof(ownerId));

            if (propertyId == Guid.Empty)
                throw new ArgumentException("PropertyId cannot be empty.", nameof(propertyId));

            if (string.IsNullOrWhiteSpace(tenantEmail))
                throw new ArgumentException("TenantEmail cannot be null or empty.", nameof(tenantEmail));

            if (rent <= 0)
                throw new ArgumentException("Rent must be greater than zero.", nameof(rent));

            if (deposit < 0)
                throw new ArgumentException("Deposit cannot be negative.", nameof(deposit));

            if (familyAllowanceFundAmount < 0)
                throw new ArgumentException("FamilyAllowanceFundAmount cannot be negative.", nameof(familyAllowanceFundAmount));

            if (startDate == default)
                throw new ArgumentException("StartDate must be a valid date.", nameof(startDate));

            if (endDate.HasValue && endDate.Value < startDate)
                throw new ArgumentException("EndDate cannot be before StartDate.", nameof(endDate));

            OwnerId = ownerId;
            PropertyId = propertyId;
            TenantId = tenantId;
            TenantEmail = tenantEmail;
            Rent = rent;
            Deposit = deposit;
            FamilyAllowanceFundAmount = familyAllowanceFundAmount;
            StartDate = startDate;
            EndDate = endDate;
            Note = note;
        }
    }
}