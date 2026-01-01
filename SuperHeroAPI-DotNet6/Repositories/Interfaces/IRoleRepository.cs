using SuperHeroAPI_DotNet6.Models.Entities;

namespace SuperHeroAPI_DotNet6.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role> GetRoleAsync(string role);
    }
}
