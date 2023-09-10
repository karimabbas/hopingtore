using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ShopingStore.Data;
using ShopingStore.Models;
using ShopingStore.Services;
using ShopingStore.ViewModel;

namespace ShopingStore.Repository
{
    public class ProductService : IMainService<Product>
    {
        private readonly MyDBContext _myDBContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductService(MyDBContext myDBContext, IWebHostEnvironment webHostEnvironment)
        {
            _myDBContext = myDBContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public void Add(Product product)
        {
            _myDBContext.Add(product);
            _myDBContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = _myDBContext.Products.Find(id);
            _myDBContext.Products.Remove(product);
            _myDBContext.SaveChanges();

        }

        public List<Product> GetAll()
        {
            return _myDBContext.Products.Include(x => x.Image).Include(x => x.Category).ToList();
        }

        public Product GetById(int id)
        {
            return _myDBContext.Products.Include(x => x.Image).FirstOrDefault(x => x.Id == id);
        }

        public List<Product> Search(string name)
        {
            return _myDBContext.Products.Where(x => x.Name.Contains(name)).Include(x => x.Image).ToList();
        }

        public void Update(Product t)
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(int id, ProductViewModel product)
        {
            var Oldproduct = _myDBContext.Products.FirstOrDefault(x => x.Id == id);
            if (Oldproduct != null)
            {
                Oldproduct.Product_Price = product.Product_Price;
                Oldproduct.Product_Size = product.Product_Size;
                Oldproduct.Category_Id = product.Category_Id;
                Oldproduct.Description = product.Description;
                // Oldproduct.Image_Id = product.Image_Id;
                Oldproduct.Product_Color = product.Product_Color;
                Oldproduct.Name = product.Name;
                Oldproduct.Popularity = product.Popularity;
                Oldproduct.Stored_Quantity = product.Stored_Quantity;

                _myDBContext.Update(Oldproduct);
                _myDBContext.SaveChanges();
            }else{
                
            }


        }


        public Image UploadFile(List<IFormFile> formFile)
        {
            List<string> imglist = new();
            foreach (var item in formFile)
            {
                var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "ProductImages");
                var filename = Path.GetFileName(item.FileName);
                var filePath = Path.Combine(uploads, filename);
                using var filestream = new FileStream(filePath, FileMode.Create);
                item.CopyTo(filestream);
                imglist.Add(filename);
            }

            Image Image = new()
            {
                Image1 = imglist[0].ToString(),
                Image2 = imglist[1].ToString(),
                Image3 = imglist[2].ToString()
            };
            _myDBContext.Images.Add(Image);
            _myDBContext.SaveChanges();
            return Image;
        }
    }
}