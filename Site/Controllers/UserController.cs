using BusinessAccessLayer.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace Site.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        public async Task<IActionResult> ProductList()
        {
            var result = await _userServices.ProductList();
            return View(result);
        }
    }
}
