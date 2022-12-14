namespace BulkyBookDataAccess.Repository.IRepository;

public interface IUnitOfWork
{
    ICategoryRepository Category { get; }
    ICoverTypeRepository CoverType { get; }
    IProductRepository Product { get; }
    void Save();
}