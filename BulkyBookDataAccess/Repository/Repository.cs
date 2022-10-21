using System.Linq.Expressions;
using BulkyBookDataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookDataAccess.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _db;
    internal DbSet<T> dbSet;

    public Repository(ApplicationDbContext db)
    {
        _db = db;
        //_db.Products.Include(u => u.Category).Include(u=>u.CoverType);
        dbSet = _db.Set<T>();
    }

    public T GetFirstOrDefault(Expression<Func<T, bool>> filter,string? includeProperties = null)
    {
        IQueryable<T> query = dbSet;
        
        query = query.Where(filter);
        if (includeProperties != null)
        {
            foreach (var includePro in includeProperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includePro);
            }
        }
        return query.FirstOrDefault();
    }

    public IEnumerable<T> GetAll(string? includeProperties = null)
    {
        IQueryable<T> query = dbSet;
        if (includeProperties != null)
        {
            foreach (var includePro in includeProperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includePro);
            }
        }
        return query.ToList();
    }

    public void Add(T entity)
    {
        dbSet.Add(entity);
    }

    public void Remove(T entity)
    {
        dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entity)
    {
        dbSet.RemoveRange(entity);
    }
}