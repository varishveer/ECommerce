using ModelAccessLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Abstraction
{
    public interface IAccountServices
    {
        public Task<bool> LoginAdmin(LoginModel loginModel);
        public Task<bool> RegisterAdmin(RegistrationModel registrationModel);

        public Task<bool> RegisterUser(RegistrationModel registrationModel);
        public Task<bool> LoginUser(LoginModel loginModel);
        public Task<bool> LogOutUser();
        public Task<string> CheckEmailForForgotPassword(CheckEmailForForgotPasswordModel checkEmailForForgotPasswordModel);
        public Task<bool> ForgotPasswordMethod(ForgotPasswordModel forgotPassword, string email);
    }
}
