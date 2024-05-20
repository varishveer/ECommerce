using BusinessAccessLayer.Abstraction;
using DataAccessLayer.DbServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ModelAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Implementation
{
	public class OrderServices : IOrderServices
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		public OrderServices(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_context = context;
			_userManager = userManager;
			_signInManager = signInManager;
		}
		// Get Method For Add To Cart
		public async Task<bool> AddProductToCart(int Id, string UserName)
		{
			var getRow = await _context.Products.Where(p => p.Id == Id).FirstOrDefaultAsync();
			if (getRow != null) 
			{	
				OrderModel orders = new OrderModel()
						{
							ProductName = getRow.Name,
							price = Convert.ToInt32(getRow.Price),
							ProductId = Id,
							quantity=1,
							Email = UserName
                };
				await _context.Orders.AddAsync(orders);
				await _context.SaveChangesAsync();
                
                return true;
            }
			return false;
        }

        public async Task<List<OrderModel>> OrderListUser(string UserName)
        {
            var getRow = await _context.Orders.Where(p =>p.Email ==UserName ).ToListAsync();
            return getRow;
        }
        public async Task<List<OrderModel>> OrderList()
		{
			var result = await _context.Orders.ToListAsync();
			return result;
		}

		public async Task<bool> AddQuantity(int Id, int Quantity)
		{
            var getRow = await _context.Orders.Where(p => p.Id == Id).FirstOrDefaultAsync();
			if(getRow != null)
			{
				getRow.quantity = Quantity;
				await _context.SaveChangesAsync();
                return true;
            }
			return false;
        }
		
		// Delete Product From Order Table
		public async Task<bool> DeleteOrder(int Id)
		{
			var getRow = await _context.Orders.Where(x => x.Id == Id).FirstOrDefaultAsync();
			if(getRow == null) return false;	
			 _context.Orders.Remove(getRow);
            await _context.SaveChangesAsync();

            return true;
		}
	}
}
