using ModelAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Abstraction
{
    public interface IProductServices
    {
        public Task<List<ProductModel>> ProductList();

        public Task<bool> AddProduct(ProductViewModel model, string path);
        public Task<bool> DeleteProduct(int Id, string path);
        public Task<bool> EditProduct(ProductViewModel model, string path);
        public Task<ProductModel> EditProduct(int id);
    }
}
