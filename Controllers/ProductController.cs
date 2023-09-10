using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopingStore.Data;
using ShopingStore.Models;
using ShopingStore.Repository;
using ShopingStore.Services;
using X.PagedList;

namespace ShopingStore.Controllers
{
    // [Route("[controller]")]
    
    public class ProductController : Controller
    {
        private readonly IMainService<Product> _mainService;
        private readonly MyDBContext _myDBContext;
        private readonly IFilterService<Product> _filterService;

        public ProductController(IMainService<Product> mainService, MyDBContext myDBContext, IFilterService<Product> filterService)
        {
            _mainService = mainService;
            _myDBContext = myDBContext;
            _filterService = filterService;

        }
        [AllowAnonymous]
        public IActionResult Indexx(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 20;
            var products = _mainService.GetAll().ToPagedList(pageNumber, pageSize);
            ViewData["Category"] = _myDBContext.Categories.ToHashSet();
            ViewData["Tags"] = _myDBContext.Tags.ToHashSet();
            return View("Index", products);

        }

        public IActionResult ProductsInCategory(int id, int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 20;
            var products = _filterService.FilterByCategory(id).ToPagedList(pageNumber, pageSize);
            ViewData["Category"] = _myDBContext.Categories.ToHashSet();
            ViewData["Tags"] = _myDBContext.Tags.ToHashSet();
            return View("Index", products);
        }

        public IActionResult SortProducts(int id, int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 16;
            var products = _filterService.SortingItems(id).ToPagedList(pageNumber, pageSize);
            ViewData["Category"] = _myDBContext.Categories.ToHashSet();
            ViewData["Tags"] = _myDBContext.Tags.ToHashSet();
            return View("Index", products);
        }
        public IActionResult ProductsWithPrice(int id, int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 16;
            var products = _filterService.FilterByPrice(id).ToPagedList(pageNumber, pageSize);
            ViewData["Category"] = _myDBContext.Categories.ToHashSet();
            ViewData["Tags"] = _myDBContext.Tags.ToHashSet();
            return View("Index", products);
        }
        public IActionResult ProductsInTags(int id, int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 16;
            var products = _filterService.FilterByTags(id).ToPagedList(pageNumber, pageSize);
            ViewData["Category"] = _myDBContext.Categories.ToHashSet();
            ViewData["Tags"] = _myDBContext.Tags.ToHashSet();
            return View("Index", products);
        }

        public IActionResult ProductSearch(string name, int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 16;
            var products = _mainService.Search(name).ToPagedList(pageNumber, pageSize);
            ViewData["Category"] = _myDBContext.Categories.ToHashSet();
            ViewData["Tags"] = _myDBContext.Tags.ToHashSet();
            return View("Index", products);
        }


        // [Authorize]
        public IActionResult Details(int id)
        {
            var product = _mainService.GetById(id);
            if (product == null) return NotFound();

            /// view Relatied products Have Same Category Id
            ViewBag.RelatedProducts = _mainService.GetAll().Where(p => p.Category_Id == product.Category_Id).Take(8).ToList();
            return View(product);

        }

        [Authorize]
        public IActionResult AddWishList(int id)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var product = _mainService.GetById(id);

            foreach (var item in _myDBContext.UserWishLists.ToList())
            {
                if (item.Product_id == product.Id)
                {
                    _myDBContext.UserWishLists.Remove(item);
                    _myDBContext.SaveChanges();
                }


            }
            var UserWishList = new UserWishList
            {
                User_id = userId,
                Product_id = product.Id
            };
            _myDBContext.UserWishLists.Add(UserWishList);
            _myDBContext.SaveChanges();
            return RedirectToAction("UserWishList");

        }

        [Authorize]
        public IActionResult UserWishList()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var whishList = _myDBContext.UserWishLists.Include(m => m.Product).ThenInclude(m => m.Image).Where(r => r.User_id == userId).ToList();

            return View(whishList);
        }

        [Authorize]
        public IActionResult DeleteFromWishList(int id)
        {
            var productFromWishList = _myDBContext.UserWishLists.FirstOrDefault(m => m.Id == id);
            _myDBContext.UserWishLists.Remove(productFromWishList);
            _myDBContext.SaveChanges();
            return RedirectToAction("UserWishList");
        }



















        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}