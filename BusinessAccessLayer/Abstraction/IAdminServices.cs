using ModelAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Abstraction
{
    public interface IAdminServices
    {
        
        public Task<List<ApplicationUser>> UserList();
        public Task<bool> DeleteUser(string email);



    }
}
