using BusinessAccessLayer.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelAccessLayer.ViewModels;

namespace ShoppingSite.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountServices _account;
        public AccountController(IAccountServices account)
        {
            _account = account;
        }
        public IActionResult Index()
        {
            return View();
        }

        
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public IActionResult RegistrationAdmin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegistrationAdmin(RegistrationModel registrationModel)
        {
            var result = await _account.RegisterAdmin(registrationModel);
            if (result)
            { return RedirectToAction("LoginAdmin", "Account"); }
            else
                return View();
        }
        [HttpGet]
        
        public IActionResult LoginAdmin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginAdmin(LoginModel loginModel)
        {
            var result = await _account.LoginAdmin(loginModel);
            if (result)
            { return RedirectToAction("Index", "Home"); }
            else
                return View();
        }


        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationModel registrationModel)
        {
            var result = await _account.RegisterUser(registrationModel);
            if (result == true)
            { return RedirectToAction("Login", "Account"); }
            else
                return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var result = await _account.LoginUser(loginModel);
            if (result)
            { return RedirectToAction("ProductListUser", "Product"); }
            else
                return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _account.LogOutUser();
            return RedirectToAction("LoginAdmin");
        }
        [HttpGet]
        public IActionResult CheckEmailForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CheckEmailForgotPassword(CheckEmailForForgotPasswordModel model)
        {
            var response = await _account.CheckEmailForForgotPassword(model);
            TempData["EmailString"] = response;
            if (response != null)
                return RedirectToAction("ForgotPassword");
            else
                return View();
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel forgotPassword)
        {
            string response;
            if (TempData.ContainsKey("EmailString"))
                response = TempData["EmailString"].ToString();
            else
                return View();

            var result = await _account.ForgotPasswordMethod(forgotPassword, response);
            if (result == true)
                return RedirectToAction("Index", "Home");
            else return View();
        }
    }
}
