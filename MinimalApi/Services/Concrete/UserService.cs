using MinimalApi.Models;
using MinimalApi.Repositories;

namespace MinimalApi.Services
{
    public class UserService : IUserService
    {
        public User Get(UserLogin userLogin)
        {
            User user = UserRepository.Users.FirstOrDefault(u => u.UserName.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase) && u.Password.Equals(userLogin.Password));
            return user; 
        }
    }
}
