using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelAccessLayer.Models
{
    public class OrderModel
    {
        [Key]
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public int price { get; set; }
        public int ProductId { get; set; }
        public int quantity { get; set; }
        public string Email { get; set; }

    }
}
