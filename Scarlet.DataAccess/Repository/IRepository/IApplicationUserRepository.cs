using Scarlet.Models;

namespace Scarlet.DataAccess.Repository.IRepository
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        public void Update(ApplicationUser category);
    }
}
