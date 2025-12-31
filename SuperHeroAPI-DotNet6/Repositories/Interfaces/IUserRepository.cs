using SuperHeroAPI_DotNet6.Models.Entities;

namespace SuperHeroAPI_DotNet6.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailOrUsernameAsync(string email, string username);
    }
}
