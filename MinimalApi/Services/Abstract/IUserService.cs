using MinimalApi.Models;

namespace MinimalApi.Services
{
    public interface IUserService
    {
        public User Get(UserLogin userLogin);
    }
}
