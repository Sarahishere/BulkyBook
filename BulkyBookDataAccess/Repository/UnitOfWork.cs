using BulkyBookDataAccess.Repository.IRepository;

namespace BulkyBookDataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    public ICategoryRepository Category { get; private set; }
    public ICoverTypeRepository CoverType { get; private set; }
    public IProductRepository Product { get; private set; }

    private ApplicationDbContext _db;
    
    public UnitOfWork(ApplicationDbContext db) 
    {
        _db = db;
        Category = new CategoryRepository(_db);
        CoverType = new CoverTypeRepository(_db);
        Product = new ProductRepository(_db);
    }
    
    public void Save()
    {
        _db.SaveChanges();
    }
}