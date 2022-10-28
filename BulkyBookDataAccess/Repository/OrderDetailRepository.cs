using BulkyBookModels;
using BulkyBookDataAccess.Repository.IRepository;

namespace BulkyBookDataAccess.Repository;

public class OrderDetailRepository : Repository<OrderDetail>,IOrderDetailRepository
{
    private readonly ApplicationDbContext _db;
    public OrderDetailRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(OrderDetail obj)
    {
        _db.OrderDetails.Update(obj);
    }
}

