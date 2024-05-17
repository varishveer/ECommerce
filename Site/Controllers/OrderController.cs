using BusinessAccessLayer.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Site.Controllers
{
	public class OrderController : Controller
	{
		private readonly IOrderServices _orderServices;
		public OrderController(IOrderServices orderServices)
		{
			_orderServices = orderServices;
		}
		public IActionResult Index()
		{
			return View();
        }
		public async Task<IActionResult> AddToCart(int Id, string email) 
		{
			await _orderServices.AddProductToCart(Id, email);
            return View();
		}
		public async Task<IActionResult> AddQuantityforProduct(int Id, int Quantity)
		{
			await _orderServices.AddQuantity(Id, Quantity);
			return View();
        }
        public async Task<IActionResult> OrderList() 
		{ 
			await _orderServices.OrderList();
			return View();
		}
		public async Task<IActionResult> Delete(int id)
		{
			await _orderServices.DeleteOrder(id);
			return View();
		}
    }
}
