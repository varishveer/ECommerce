using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelAccessLayer.Models
{
    public class ProductViewModel
    {
        public ProductModel? ProductModel { get; set; }
        public IFormFile? Image { get; set; }
    }
}
