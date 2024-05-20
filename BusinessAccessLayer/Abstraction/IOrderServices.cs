using ModelAccessLayer.Models;

namespace BusinessAccessLayer.Abstraction
{
    public interface IOrderServices
	{
		public Task<bool> AddProductToCart(int Id, string email);
        public Task<bool> AddQuantity(int Id, int Quantity);
        public Task<List<OrderModel>> OrderList();
        public Task<List<OrderModel>> OrderListUser(string UserName);

        public Task<bool> DeleteOrder(int Id);
    }
}
