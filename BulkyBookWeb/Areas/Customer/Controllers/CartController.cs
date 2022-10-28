using System.Security.Claims;
using BulkyBookDataAccess.Repository.IRepository;
using BulkyBookModels;
using BulkyBookModels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers;

[Area("Customer")]
[Authorize]
public class CartController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public ShoppingCartVM ShoppingCartVm { get; set; }
    

    public CartController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public IActionResult Index()
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        ShoppingCartVm = new()
        {
            ListCart = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value,
                includeProperties:"Product"),
            OrderHeader = new OrderHeader()
        };
        foreach (var cart in ShoppingCartVm.ListCart)
        {
            cart.Price = GetPriceByQuantity(cart.Count, cart.Product.Price, cart.Product.Price50,
                cart.Product.Price100);
            ShoppingCartVm.OrderHeader.OrderTotal += (cart.Price * cart.Count);
        }
        return View(ShoppingCartVm);
    }

    public IActionResult Plus(int cartId)
    {
        var cartFromDb = _unitOfWork.ShoppingCart.GetFirstOrDefault(u=>u.Id == cartId);
        _unitOfWork.ShoppingCart.IncrementCount(cartFromDb, 1);
        _unitOfWork.Save();
        return RedirectToAction("Index");
    }
    
    public IActionResult Minus(int cartId)
    {
        var cartFromDb = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
        if (cartFromDb.Count <= 1)
        {
            _unitOfWork.ShoppingCart.Remove(cartFromDb);
        }
        _unitOfWork.ShoppingCart.DecrementCount(cartFromDb,1);
        _unitOfWork.Save();
        return RedirectToAction("Index");
    }

    public IActionResult Remove(int cartId)
    {
        var cartFromDb = _unitOfWork.ShoppingCart.GetFirstOrDefault(u=>u.Id == cartId);
        _unitOfWork.ShoppingCart.Remove(cartFromDb);
        _unitOfWork.Save();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Summary()
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        ShoppingCartVm = new()
        {
            ListCart = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value,
                includeProperties: "Product"),
            OrderHeader = new()
        };

        ShoppingCartVm.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(
            u=>u.Id == claim.Value);
        ShoppingCartVm.OrderHeader.Name = ShoppingCartVm.OrderHeader.ApplicationUser.Name;
        ShoppingCartVm.OrderHeader.PhoneNumber = ShoppingCartVm.OrderHeader.ApplicationUser.PhoneNumber;
        ShoppingCartVm.OrderHeader.StreetAddress = ShoppingCartVm.OrderHeader.ApplicationUser.StreetAddress;
        ShoppingCartVm.OrderHeader.City = ShoppingCartVm.OrderHeader.ApplicationUser.City;
        ShoppingCartVm.OrderHeader.State = ShoppingCartVm.OrderHeader.ApplicationUser.State;
        ShoppingCartVm.OrderHeader.PostalCode = ShoppingCartVm.OrderHeader.ApplicationUser.PostalCode;

        foreach (var cart in ShoppingCartVm.ListCart)
        {
            cart.Price = GetPriceByQuantity(cart.Count, cart.Product.Price, cart.Product.Price50,
                cart.Product.Price100);
        }
        
        
        return View();
    }

    private double GetPriceByQuantity(double quantity, double price,double price50,double price100)
    {
        if (quantity <= 50)
        {
            return price;
        }
        else if (quantity <= 100)
        {
            return price50;
        }

        return price100;
    }
}