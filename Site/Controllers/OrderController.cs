using Microsoft.AspNetCore.Mvc;

namespace Site.Controllers
{
	public class OrderController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult AddToProduct() 
		{
			return View();
		}
	}
}
