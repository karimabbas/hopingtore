using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopingStore.Data;
using ShopingStore.Models;
using ShopingStore.Services;
using ShopingStore.ViewModel;

namespace ShopingStore.Repository
{
    public class CategoryService : IMainService<Category>
    {
        private readonly MyDBContext _myDBContext;
        public CategoryService(MyDBContext myDBContext)
        {
            _myDBContext = myDBContext;
        }
        public void Add(Category Category)
        {
            _myDBContext.Add(Category);
            _myDBContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var category = _myDBContext.Categories.Find(id);
            _myDBContext.Categories.Remove(category);
            _myDBContext.SaveChanges();

        }

        public List<Category> GetAll()
        {
            return _myDBContext.Categories.ToList();
        }

        public Category GetById(int id)
        {
            return _myDBContext.Categories.Find(id);
        }

        public List<Category> Search(string name)
        {
            return _myDBContext.Categories.Where(x => x.Name.Contains(name)).ToList();
        }

        public void Update(Category Category)
        {
            _myDBContext.Update(Category);
            _myDBContext.SaveChanges();
        }

        public void UpdateProduct(int i, Category t)
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(int i, ProductViewModel t)
        {
            throw new NotImplementedException();
        }

        public Image UploadFile(List<IFormFile> formFile)
        {
            throw new NotImplementedException();
        }
    }
}