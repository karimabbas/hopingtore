using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopingStore.Data;
using ShopingStore.Models;

namespace ShopingStore.Controllers;

public class HomeController : Controller
{
    private readonly MyDBContext _myDBContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HomeController(MyDBContext myDBContext, IHttpContextAccessor httpContextAccessor)
    {
        _myDBContext = myDBContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public IActionResult Index()
    {
        var BestSeller = _myDBContext.Products.Include(m => m.Image).OrderBy(m => m.Stored_Quantity).Take(8).ToList();
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var whishList = _myDBContext.UserWishLists.Where(r => r.User_id == userId).Count();
        var order = _myDBContext.Orders.FirstOrDefault(x => x.Customer_Id == userId);
        var CartItems = 0;
        if (order != null)
        {
            CartItems = _myDBContext.OrderDetails.Where(x => x.Order_Id == order.Id).Count();

        }
        else
        {
            CartItems = 0;

        }

        _httpContextAccessor?.HttpContext?.Response.Cookies.Append("someKey", whishList.ToString());
        _httpContextAccessor?.HttpContext?.Session.SetInt32("WhishList", whishList);
        _httpContextAccessor?.HttpContext?.Session.SetInt32("CartItems", CartItems);

        return View(BestSeller);
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
