using Microsoft.EntityFrameworkCore;
using SuperHeroAPI_DotNet6.Data;
using SuperHeroAPI_DotNet6.Models.Entities;
using SuperHeroAPI_DotNet6.Repositories.Interfaces;

namespace SuperHeroAPI_DotNet6.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;

        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<User?> GetUserByEmailOrUsernameAsync(string email, string username)
        {
            return await _dataContext.Users
                .FirstOrDefaultAsync(
                    u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) ||
                           u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
        }
    }
}
