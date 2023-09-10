using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopingStore.Models;
using ShopingStore.ViewModel;

namespace ShopingStore.Services
{
    public interface IMainService<T> where T : class
    {
        List<T> GetAll();
        T GetById(int id);
        void Add(T t);
        void Update(T t);
        void UpdateProduct(int i, ProductViewModel t);

        void Delete(int id);
        List<T> Search(string name);
Image UploadFile(List<IFormFile> formFile);
    }
}