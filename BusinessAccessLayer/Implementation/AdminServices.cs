using BusinessAccessLayer.Abstraction;
using DataAccessLayer.DbServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ModelAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Implemention
{
    public class AdminServices : IAdminServices
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;



        public AdminServices(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<ApplicationUser>>  UserList()
        {
            var users = await _userManager.Users.ToListAsync();
            return users; 
        }

        public async Task<bool> DeleteUser(string email) 
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
    }
}
