using Scarlet.Models;

namespace Scarlet.DataAccess.Repository.IRepository
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        void Update(ApplicationUser category);
    }
}
