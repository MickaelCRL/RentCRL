using RentCRL.Domain.Results;
using RentCRL.Domain.Users;

namespace RentCRL.Application.Users
{
    public interface IUserService
    {
        Task<Result<User>> GetUserByEmailAsync(string email);
    }
}
