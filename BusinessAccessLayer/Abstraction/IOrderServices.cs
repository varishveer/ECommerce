using ModelAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Abstraction
{
	public interface IOrderServices
	{
		public Task<bool> AddProductToCart(int Id, string email);
        public Task<bool> AddQuantity(int Id, int Quantity);
        public Task<List<OrderModel>> OrderList();
        public Task<bool> DeleteOrder(int Id);
    }
}
