using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopingStore.Models;
using ShopingStore.Services;

namespace ShopingStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="admin")]
    public class CategoryController : Controller
    {
        private readonly IMainService<Category> _categoryService;
        public CategoryController(IMainService<Category> categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var Categories = _categoryService.GetAll();
            return View(Categories);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var item = _categoryService.GetById(id);
            
            return View(item);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var category = _categoryService.GetById(id);
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
            _categoryService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Admin/Category/Create")]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("Id,Name,Description,Created,Update")] Category category)
        {
            if (!ModelState.IsValid) return View(category);
            _categoryService.Add(category);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Admin/Category/Edit/id:int")]
        public IActionResult Edit(int id)
        {
            var category = _categoryService.GetById(id);
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id,Name,Description,Created,Update")] Category category)
        {
            var EditCatg = _categoryService.GetById(id);
            if (EditCatg == null) return NotFound();

            if (ModelState.IsValid)
            {
                EditCatg.Name = category.Name;
                EditCatg.Description = category.Description;
                EditCatg.Update = category.Update;
                EditCatg.Created = EditCatg.Created;

                _categoryService.Update(EditCatg);

            }
            else
            {
                ModelState.AddModelError("", "Not Valid");
                return View(category);
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