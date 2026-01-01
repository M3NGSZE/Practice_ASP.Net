using SuperHeroAPI_DotNet6.Data;
using SuperHeroAPI_DotNet6.Models.Entities;
using SuperHeroAPI_DotNet6.Repositories.Interfaces;

namespace SuperHeroAPI_DotNet6.Repositories.Implementations
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _dataContext;

        public AuthRepository(DataContext dataContext) 
        { 
            _dataContext = dataContext;
        }

        public async Task<User> CreateAsync(User user)
        {
            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();
            return user;
        }
    }
}
