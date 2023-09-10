using System.Net.Mime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using ShopingStore.Data;
using ShopingStore.Migrations;
using ShopingStore.Models;
using ShopingStore.Services;
using ShopingStore.ViewModel;

namespace ShopingStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class ProductController : Controller
    {
        private readonly MyDBContext _myDBContext;
        private readonly IMainService<Product> _ProductService;
        private readonly IMainService<Category> _CategoryService;

        public ProductController(MyDBContext myDBContext, IMainService<Product> ProductService, IMainService<Category> CategoryService)
        {
            _myDBContext = myDBContext;
            _ProductService = ProductService;
            _CategoryService = CategoryService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var Products = _ProductService.GetAll();
            ViewBag.Categories = _CategoryService.GetAll();
            return View(Products);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var item = _ProductService.GetById(id);

            return View(item);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var category = _ProductService.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
            // return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            _ProductService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = _CategoryService.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    Name = productVM.Name,
                    Product_Color = productVM.Product_Color,
                    Product_Price = productVM.Product_Price,
                    Product_Size = productVM.Product_Size,
                    Description = productVM.Description,
                    Adding_date = productVM.Adding_date,
                    Category_Id = productVM.Category_Id,
                    Popularity = productVM.Popularity,
                    Stored_Quantity = productVM.Stored_Quantity,
                };
                var images = _ProductService.UploadFile(productVM.Formfile);
                product.Image_Id = images.Id;
                _ProductService.Add(product);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "some Errors Ocuured");
            }
            return View(productVM);

        }

        [HttpGet]

        public IActionResult Edit(int id)
        {
            var product = _ProductService.GetById(id);
            ViewBag.Categories = _CategoryService.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            if (product == null) return NotFound();

            var productVM = new ProductViewModel
            {
                Name = product.Name,
                Product_Color = product.Product_Color,
                Product_Price = product.Product_Price,
                Product_Size = product.Product_Size.Value,
                Description = product.Description,
                Category_Id = product.Category_Id,
                Popularity = product.Popularity,
                Stored_Quantity = product.Stored_Quantity,
                image1 = product.Image?.Image1,
                image2 = product.Image?.Image2,
                image3 = product.Image?.Image3,

            };

            return View(productVM);
        }

        [HttpPost]
        public IActionResult Edit(int id, ProductViewModel editViewModel)
        {

            var product = _ProductService.GetById(id);
            if (product == null) return NotFound();

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Error");
                return View(editViewModel);
            }
            else
            {
                if (editViewModel.Formfile?.Count > 0)
                {
                    var images = _ProductService.UploadFile(editViewModel.Formfile);
                    product.Image_Id = images.Id;
                }
                else
                {
                    product.Image_Id = product.Image_Id;
                }

                _ProductService.UpdateProduct(id, editViewModel);

            }
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}