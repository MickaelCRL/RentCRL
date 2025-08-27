namespace RentCRL.Domain
{
    public record Address
    {
        public string Line1 { get; }
        public string Line2 { get; }
        public string PostalCode { get; }
        public string City { get; }
        public string Country { get; }

        public Address(
            string line1,
            string line2,
            string postalCode,
            string city,
            string country
        )
        {
            ArgumentException.ThrowIfNullOrEmpty(line1, nameof(line1));
            ArgumentException.ThrowIfNullOrEmpty(postalCode, nameof(postalCode));
            ArgumentException.ThrowIfNullOrEmpty(city, nameof(city));
            ArgumentException.ThrowIfNullOrEmpty(country, nameof(country));

            Line1 = line1;
            Line2 = line2;
            PostalCode = postalCode;
            City = city;
            Country = country;
        }
    }
}
