using BulkyBookModels;

namespace BulkyBookDataAccess.Repository.IRepository;

public class CompanyRepository : Repository<Company>,ICompanyRepository
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