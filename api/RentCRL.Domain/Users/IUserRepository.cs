using RentCRL.Domain.Base;

namespace RentCRL.Domain.Users
{
    public interface IUserRepository : IEntityRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
    }
}
