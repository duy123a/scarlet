using Scarlet.DataAccess.Data;
using Scarlet.DataAccess.Repository.IRepository;
using Scarlet.Models;

namespace Scarlet.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Company obj)
        {
            _db.Companies.Update(obj);
        }
    }
}
