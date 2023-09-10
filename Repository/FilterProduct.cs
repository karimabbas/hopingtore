using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopingStore.Data;
using ShopingStore.Models;
using ShopingStore.Services;

namespace ShopingStore.Repository
{
    public class FilterProduct : IFilterService<Product>
    {
        private readonly MyDBContext _myDBContext;
        public FilterProduct(MyDBContext myDBContext)
        {
            _myDBContext = myDBContext;

        }
        public List<Product> FilterByCategory(int id)
        {
            return _myDBContext.Products.Include(m => m.Image).Where(c => c.Category_Id == id).ToList();
        }

        public List<Product> FilterByPrice(int price)
        {
            List<Product> products;
            switch (price)
            {
                case (1):
                    products = _myDBContext.Products.Include(model => model.Image).Where(model => model.Product_Price > 0 && model.Product_Price <= 100).ToList();
                    break;
                case (2):
                    products = _myDBContext.Products.Include(model => model.Image).Where(model => model.Product_Price > 100 && model.Product_Price <= 150).ToList();
                    break;
                case (3):
                    products = _myDBContext.Products.Include(model => model.Image).Where(model => model.Product_Price > 150 && model.Product_Price <= 200).ToList();
                    break;
                case (4):
                    products = _myDBContext.Products.Include(model => model.Image).Where(model => model.Product_Price > 200).ToList();
                    break;
                default:
                    products = _myDBContext.Products.Include(model => model.Image).ToList();
                    break;
            }
            return products;
        }

        public List<Product> FilterByTags(int id)
        {
            return _myDBContext.ProductTags.Where(m=>m.Tag_Id == id).Include(m=>m.Product).ThenInclude(c=>c.Image).Select(m=>m.Product).ToList();
        }

        public List<Product> SortingItems(int id)
        {
            List<Product> products;
            switch (id)
            {
                case (1):
                    products = _myDBContext.Products.Include(model => model.Image)
                            .OrderByDescending(model => model.Popularity).ToList();
                    break;
                case (2):
                    products = _myDBContext.Products.Include(model => model.Image)
                            .OrderByDescending(model => model.Adding_date).ToList();
                    break;
                case (3):
                    products = _myDBContext.Products.Include(model => model.Image)
                            .OrderBy(model => model.Product_Price).ToList();
                    break;
                case (4):
                    products = _myDBContext.Products.Include(model => model.Image)
                            .OrderByDescending(model => model.Product_Price).ToList();
                    break;
                default:
                    products = _myDBContext.Products.Include(model => model.Image).ToList();
                    break;
            }
            return products;
        }
    }
}