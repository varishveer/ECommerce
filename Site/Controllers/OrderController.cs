using BusinessAccessLayer.Abstraction;
using Microsoft.AspNetCore.Authorization;
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
		[HttpGet]
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> AddToCart(int Id, string userName) 
		{	

             await _orderServices.AddProductToCart(Id, userName);
            var result =await  _orderServices.OrderListUser(userName);
            return View(result);
        }
		[HttpGet]
		public async Task<IActionResult> Cart(string userName)
		{
            var result = await _orderServices.OrderListUser(userName);
            return View(result);

        }
        [HttpGet]
		[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> OrderList()
        {
            var result =await _orderServices.OrderList();
            return View(result);
        }
        public async Task<IActionResult> AddQuantityforProduct(int Id, int Quantity)
		{
			await _orderServices.AddQuantity(Id, Quantity);
			return View();
        }
        
		public async Task<IActionResult> Delete(int id)
		{
			await _orderServices.DeleteOrder(id);
            return RedirectToAction(nameof(Cart));
        }

        public async Task<IActionResult> DeleteByAdmin(int id)
        {
            await _orderServices.DeleteOrder(id);
            return RedirectToAction(nameof(OrderList));
        }

    }
}
