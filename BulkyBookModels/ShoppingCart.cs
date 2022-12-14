using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace BulkyBookModels;

public class ShoppingCart
{
    public Product Product { get; set; }
    [Range(1,1000,ErrorMessage="Please enter a value between 1 and 1000")]
    public int Count { get; set; }
}