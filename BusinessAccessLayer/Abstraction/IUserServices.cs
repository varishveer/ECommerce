using ModelAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Abstraction
{
    public interface IUserServices
    {
        public Task<List<ProductModel>> ProductList();

    }
}
