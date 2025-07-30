namespace RentCRL.Infrastructure.Database
{
    public static class ContainersNames
    {
        public const string Entities = "Entities";

        public static readonly IReadOnlyDictionary<string, string> PartitionKeys = new Dictionary<string, string>
        {
            { Entities, "/id" },
        }; 
    }
}
