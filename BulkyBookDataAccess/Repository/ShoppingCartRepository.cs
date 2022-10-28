using BulkyBookDataAccess.Repository.IRepository;
using BulkyBookModels;

namespace BulkyBookDataAccess.Repository;

public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
{
    private readonly ApplicationDbContext _db;
    public ShoppingCartRepository(ApplicationDbContext db) : base(db)
    {
    }

    public void Update(ShoppingCart obj)
    {
        _db.ShoppingCarts.Update(obj);
    }

    public int IncrementCount(ShoppingCart shoppingCart, int count)
    {
        shoppingCart.Count += count;
        return shoppingCart.Count;
    }

    public int DecrementCount(ShoppingCart shoppingCart, int count)
    {
        shoppingCart.Count -= count;
        return shoppingCart.Count;
    }
}