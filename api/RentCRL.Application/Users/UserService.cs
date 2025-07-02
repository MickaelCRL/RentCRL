using RentCRL.Domain.Results;
using RentCRL.Domain.Users;

namespace RentCRL.Application.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Result<User>> GetUserByEmailAsync(string email)
        {
            var response = _userRepository.GetByEmailAsync(email);
            if (response.Result == null)
                return Result.Failure<User>(UserErrors.CouldNotFindUserWithEmail);

            return await response;
        }
    }
}
