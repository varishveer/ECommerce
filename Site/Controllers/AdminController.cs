using BusinessAccessLayer.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelAccessLayer.Models;

namespace ShoppingSite.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminServices _services;
       

        public AdminController(IAdminServices services)
        {
            _services = services;
            
        }

        
        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            var result = await _services.UserList();
            return View(result);
        }

        public async Task<IActionResult> DeleteUser( string email)
        {
            await _services.DeleteUser(email);
            return RedirectToAction(nameof(UserList));
        }

    }
}
