/*using BusinessAccessLayer.Abstraction;
using Microsoft.AspNetCore.Identity;
using ModelAccessLayer.Models;
using ModelAccessLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Implementation
{
    public class AccountServices:IAccountServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountServices(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<bool> RegisterAdmin(RegistrationModel registrationModel)
        {
            var checkEmail = await _userManager.FindByEmailAsync(registrationModel.Email);
            if (checkEmail != null)
            { return false; }
            var user = new ApplicationUser()
            {
                FirstName=registrationModel.FirstName,
                LastName= registrationModel.LastName,
                UserName = registrationModel.UserName,
                Email = registrationModel.Email,
                PasswordHash = registrationModel.Password,
                PhoneNumber = registrationModel.PhoneNumber,
                DOB = registrationModel.DOB,
                Gender = registrationModel.Gender
            };
            var result = await _userManager.CreateAsync(user, registrationModel.Password);
           // var roleResult = await _roleManager.CreateAsync(new IdentityRole("ADMIN"));
            var userRole = await _userManager.AddToRoleAsync(user, "ADMIN");
            
            if (result.Succeeded &&  userRole.Succeeded)
            { return true; }
            else
                return false;
        }

        public async Task<bool> LoginAdmin(LoginModel loginModel)
        {
            try
            {

                // Find the user by email
                var user = await _userManager.FindByEmailAsync(loginModel.Email);
                if (user == null)
                {
                    // User not found
                    return false;
                }

                var admin = await _userManager.IsInRoleAsync(user, "ADMIN");
                if (admin == true)
                { // Check if the password is correct
                    var isPasswordValid = await _signInManager.CheckPasswordSignInAsync(user, loginModel.Password, lockoutOnFailure: false);
                    if (!isPasswordValid.Succeeded)
                    {
                        // Password is incorrect
                        return false;
                    }

                    await _signInManager.SignInAsync(user, loginModel.RememberMe);

                    return true;

                }
                else
                    return false;
                    
            }
            catch (Exception ex)
            {
                // Log the exception
                return false;
            }
        }

        public async Task<bool> RegisterUser(RegistrationModel registrationModel)
        {
            var checkEmail = await _userManager.FindByEmailAsync(registrationModel.Email);
            if (checkEmail != null)
            { return false; }
            var user = new ApplicationUser()
            {
                FirstName = registrationModel.FirstName,
                LastName = registrationModel.LastName,
                UserName = registrationModel.UserName,
                Email = registrationModel.Email,
                PasswordHash = registrationModel.Password,
                PhoneNumber = registrationModel.PhoneNumber,
                DOB = registrationModel.DOB,
                Gender = registrationModel.Gender
            };
            var result = await _userManager.CreateAsync(user, registrationModel.Password);
            var userRole = await _userManager.AddToRoleAsync(user, "USER");

            if (result.Succeeded && userRole.Succeeded)
            { return true; }
            else
                return false;
        }

        public async Task<bool> LoginUser(LoginModel loginModel)
        {
            try
            {
                // Find the user by email
                var user = await _userManager.FindByEmailAsync(loginModel.Email);
                if (user == null)
                {
                    // User not found
                    return false;
                }
                var admin = await _userManager.IsInRoleAsync(user, "USER");
                if (admin == true)
                {
                    // Check if the password is correct
                    var isPasswordValid = await _signInManager.CheckPasswordSignInAsync(user, loginModel.Password, lockoutOnFailure: false);
                    if (!isPasswordValid.Succeeded)
                    {
                        // Password is incorrect
                        return false;
                    }

                    // Sign in the user
                    await _signInManager.SignInAsync(user, loginModel.RememberMe);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                // Log the exception
                return false;
            }
        }

        public async Task<bool> LogOutUser()
        {
            await _signInManager.SignOutAsync();
            return true;
        }

        public async Task<string> CheckEmailForForgotPassword(CheckEmailForForgotPasswordModel checkEmailForForgotPasswordModel)
        {
            var user = await _userManager.FindByEmailAsync(checkEmailForForgotPasswordModel.Email);
            if (user == null)
            {
                // User not found
                return null;
            }
            return checkEmailForForgotPasswordModel.Email;
        }
        public async Task<bool> ForgotPasswordMethod(ForgotPasswordModel forgotPassword, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, forgotPassword.Password);
            if (result.Succeeded) { return true; }
            else return false;

        }

    }
}
*/

using BusinessAccessLayer.Abstraction;
using Microsoft.AspNetCore.Identity;
using ModelAccessLayer.Models;
using ModelAccessLayer.ViewModels;
using System;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Implementation
{
    public class AccountServices : IAccountServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountServices(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<bool> RegisterAdmin(RegistrationModel registrationModel)
        {
            var checkEmail = await _userManager.FindByEmailAsync(registrationModel.Email);
            if (checkEmail != null) return false;

            var user = new ApplicationUser
            {
                FirstName = registrationModel.FirstName,
                LastName = registrationModel.LastName,
                UserName = registrationModel.UserName,
                Email = registrationModel.Email,
                PhoneNumber = registrationModel.PhoneNumber,
                DOB = registrationModel.DOB,
                Gender = registrationModel.Gender
            };

            var result = await _userManager.CreateAsync(user, registrationModel.Password);

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("ADMIN"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("ADMIN"));
                }

                var userRole = await _userManager.AddToRoleAsync(user, "ADMIN");
                return userRole.Succeeded;
            }

            return false;
        }

        public async Task<bool> LoginAdmin(LoginModel loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user == null) return false;

            if (!await _userManager.IsInRoleAsync(user, "ADMIN")) return false;

            var isPasswordValid = await _signInManager.PasswordSignInAsync(user, loginModel.Password, loginModel.RememberMe, lockoutOnFailure: false);
            return isPasswordValid.Succeeded;
        }

        public async Task<bool> RegisterUser(RegistrationModel registrationModel)
        {
            var checkEmail = await _userManager.FindByEmailAsync(registrationModel.Email);
            if (checkEmail != null) return false;

            var user = new ApplicationUser
            {
                FirstName = registrationModel.FirstName,
                LastName = registrationModel.LastName,
                UserName = registrationModel.UserName,
                Email = registrationModel.Email,
                PhoneNumber = registrationModel.PhoneNumber,
                DOB = registrationModel.DOB,
                Gender = registrationModel.Gender
            };

            var result = await _userManager.CreateAsync(user, registrationModel.Password);

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("USER"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("USER"));
                }

                var userRole = await _userManager.AddToRoleAsync(user, "USER");
                return userRole.Succeeded;
            }

            return false;
        }

        public async Task<bool> LoginUser(LoginModel loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user == null) return false;

            if (!await _userManager.IsInRoleAsync(user, "USER")) return false;

            var isPasswordValid = await _signInManager.PasswordSignInAsync(user, loginModel.Password, loginModel.RememberMe, lockoutOnFailure: false);
            return isPasswordValid.Succeeded;
        }

        public async Task<bool> LogOutUser()
        {
            await _signInManager.SignOutAsync();
            return true;
        }

        public async Task<string> CheckEmailForForgotPassword(CheckEmailForForgotPasswordModel checkEmailForForgotPasswordModel)
        {
            var user = await _userManager.FindByEmailAsync(checkEmailForForgotPasswordModel.Email);
            return user == null ? null : checkEmailForForgotPasswordModel.Email;
        }

        public async Task<bool> ForgotPasswordMethod(ForgotPasswordModel forgotPassword, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, forgotPassword.Password);
            return result.Succeeded;
        }
    }
}
