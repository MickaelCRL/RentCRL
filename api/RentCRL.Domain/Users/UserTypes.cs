namespace RentCRL.Domain.Users
{
    public static class UserTypes
    {
        private static readonly IReadOnlyCollection<string> _allTypes =
        [
            nameof(Tenant),
            nameof(Owner)
        ];

        public static IReadOnlyCollection<string> AllTypes { get => _allTypes; }
    }
}
