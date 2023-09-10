using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopingStore.Data;
using ShopingStore.Migrations;
using ShopingStore.Models;
using ShopingStore.Services;

namespace ShopingStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IMainService<Product> _mainService;
        private readonly MyDBContext _myDBContext;
        private readonly UserManager<IdentityUser> _userManager;
        public OrderController(IMainService<Product> mainService, MyDBContext myDBContext, UserManager<IdentityUser> userManager)
        {
            _mainService = mainService;
            _myDBContext = myDBContext;
            _userManager = userManager;
        }

        [NonAction]
        public decimal CalcualteTotal(ICollection<OrderDetails> orderDetails)
        {
            decimal Result = 0;
            if (orderDetails.Count != 0)
            {
                foreach (var item in orderDetails)
                {
                    Result += item.Total_price;

                }
            }
            return Result;
        }

        [HttpPost]
        public IActionResult AddToCart(int id, int quantity, [Bind(include: "Product_Color , Product_Size")] Product prod)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var product = _mainService.GetById(id);
            var orders = _myDBContext.Orders.ToList();

            bool found = false;

            foreach (var item in orders)
            {
                if (item.Customer_Id == userId)
                {
                    found = true;
                    break;
                }
                else
                {
                    continue;
                }
            }
            if (found == false)
            {
                var order = new Order()
                {
                    Customer_Id = userId,
                    Order_Status = 0,
                    Order_Total = product.Product_Price * quantity,
                };
                _myDBContext.Orders.Add(order);
                _myDBContext.SaveChanges();

                var orderDetails = new OrderDetails()
                {
                    Order_Id = order.Id,
                    Product_Id = product.Id,
                    Product_Quantity = quantity,
                    Product_Color = prod.Product_Color,
                    Product_Size = prod.Product_Size
                };
                _myDBContext.OrderDetails.Add(orderDetails);
                _myDBContext.SaveChanges();
                return RedirectToAction("Details", "Product", new { id = product.Id });

            }
            else
            {
                var order = _myDBContext.Orders.Include(m => m.OrderDetails).FirstOrDefault(x => x.Customer_Id == userId);

                bool isFound = false;

                foreach (var item in order.OrderDetails)
                {
                    if (item.Product_Id == product.Id)
                    {
                        isFound = true;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                if (isFound)
                {
                    var orderDetails = _myDBContext.OrderDetails.FirstOrDefault(m => m.Product_Id == product.Id);

                    orderDetails.Product_Quantity = quantity;
                    orderDetails.Total_price = orderDetails.Product_Quantity * product.Product_Price;
                    _myDBContext.SaveChanges();

                    order.Order_Total = CalcualteTotal(order.OrderDetails);
                    _myDBContext.SaveChanges();
                    return RedirectToAction("Details", "Product", new { id = product.Id });

                }
                else
                {
                    OrderDetails orderDetails = new OrderDetails()
                    {
                        Order_Id = order.Id,
                        Product_Id = product.Id,
                        Product_Quantity = quantity,
                        Total_price = product.Product_Price * quantity,
                        Product_Color = prod.Product_Color,
                        Product_Size = prod.Product_Size
                    };
                    _myDBContext.OrderDetails.Add(orderDetails);
                    _myDBContext.SaveChanges();

                    order.Order_Total = CalcualteTotal(order.OrderDetails);
                    _myDBContext.SaveChanges();
                    return RedirectToAction("Details", "Product", new { id = product.Id });
                }

            }

        }

        public IActionResult Cart()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var Order = _myDBContext.Orders.Include(m => m.OrderDetails).ThenInclude(m => m.Product).ThenInclude(m => m.Image).FirstOrDefault(x => x.Customer_Id == userId);

            if (Order == null)
            {
                var neworder = new Order
                {
                    Customer_Id = userId
                };

                _myDBContext.Orders.Add(neworder);
                _myDBContext.SaveChanges();
            }
            ViewBag.Total = Order?.Order_Total + 10;
            return View(Order);
        }

        public IActionResult UpdateCart(List<OrderDetails> orderDetails)
        {

            var userid = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var Old_Order = _myDBContext.Orders.Include(x => x.OrderDetails).ThenInclude(x => x.Product).FirstOrDefault(x => x.Customer_Id == userid);

            for (int i = 0; i < Old_Order?.OrderDetails?.Count; i++)
            {
                if (orderDetails[i] != Old_Order.OrderDetails[i])
                {
                    Old_Order.OrderDetails[i].Product_Quantity = orderDetails[i].Product_Quantity;
                    // Old_Order.OrderDetails[i].Product_Color = orderDetails[i].Product_Color;
                    Old_Order.OrderDetails[i].Total_price = orderDetails[i].Product_Quantity * Old_Order.OrderDetails[i].Product.Product_Price;
                    _myDBContext.SaveChanges();
                    Old_Order.Order_Total = CalcualteTotal(Old_Order.OrderDetails);
                    _myDBContext.SaveChanges();
                }

            }
            return RedirectToAction("Cart");

        }
        public IActionResult DeleteFromCart(int id)
        {
            var orderDetails = _myDBContext.OrderDetails.Include(m => m.Order).FirstOrDefault(x => x.Id == id);

            orderDetails.Order.Order_Total -= orderDetails.Total_price;
            _myDBContext.SaveChanges();
            _myDBContext.OrderDetails.Remove(orderDetails);
            _myDBContext.SaveChanges();
            return RedirectToAction("Cart");

        }

        [HttpPost]
        public IActionResult CheckOut(Shipping shipping)
        {
            var newShipping = new Shipping
            {
                Address = shipping.Address,
                Postal_Code = shipping.Postal_Code,
                Phone = shipping.Phone,
                Shipper_Email = shipping.Shipper_Email,
                Shipper_Name = shipping.Shipper_Name,
            };
            _myDBContext.Shippings.Add(newShipping);
            _myDBContext.SaveChanges();

            var UserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var order = _myDBContext.Orders.FirstOrDefault(m => m.Customer_Id == UserId);

            order.Shipping_Id = newShipping.Id;
            _myDBContext.SaveChanges();

            var orderDetails = _myDBContext.OrderDetails.Include(m => m.Product).Where(m => m.Order_Id == order.Id).ToList();

            foreach (var item in orderDetails)
            {
                if (item.Order?.Shipping_Id != null)
                    item.Product.Stored_Quantity -= item.Product_Quantity;
            }
            _myDBContext.SaveChanges();
            return RedirectToAction("Index", "Home");

        }

    }
}