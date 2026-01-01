using Microsoft.EntityFrameworkCore;
using SuperHeroAPI_DotNet6.Data;
using SuperHeroAPI_DotNet6.Models.Entities;
using SuperHeroAPI_DotNet6.Repositories.Interfaces;

namespace SuperHeroAPI_DotNet6.Repositories.Implementations
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _dataContext;

        public RoleRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Role?> GetRoleAsync(string role)
        {
            return await _dataContext.Roles.FirstOrDefaultAsync(r => r.Name.Equals(role));
        }
    }
}
