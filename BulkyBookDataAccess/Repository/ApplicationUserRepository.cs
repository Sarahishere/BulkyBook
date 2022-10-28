using BulkyBookDataAccess.Repository.IRepository;
using BulkyBookModels;

namespace BulkyBookDataAccess.Repository;

public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
{
    private readonly ApplicationDbContext _db;
    public ApplicationUserRepository(ApplicationDbContext db) : base(db)
    {
    }
    
}