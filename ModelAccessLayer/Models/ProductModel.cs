using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelAccessLayer.Models
{
    public  class ProductModel
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? AddedDate { get; set; }
        public string? Availability { get; set; }
        public string? Price { get; set; }
        public string? Category { get; set; }
        public string? ImageURL { get; set;}
    }
}
