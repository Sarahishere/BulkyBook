using BulkyBookModels;

namespace BulkyBookDataAccess.Repository.IRepository;

public interface IProductRepository : IRepository<Product>
{
    void Update(Product obj);
}