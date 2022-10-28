using BulkyBookModels;

namespace BulkyBookDataAccess.Repository.IRepository;

public interface ICompanyRepository : IRepository<Company>
{
    void Update(Company obj);
}