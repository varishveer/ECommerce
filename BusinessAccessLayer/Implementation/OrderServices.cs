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
		public OrderServices(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		public async Task<bool> addProductToCart(int Id)
		{
			var getRow = await _context.Products.Where(p => p.Id == Id).FirstOrDefaultAsync();
			if (getRow != null) 
			{
				/*List<OrderModel> orders = new List<OrderModel>()
                {
                    ProductName = getRow.Name,
                    Price = getRow.Price,
                    ProductId = getRow.Id,
                    Quantity = 1
                };


                await _context.Add(orders);*/
			}
			_context.SaveChanges();
			return true;
		}
		public async Task<List<OrderModel>> OrderList()
		{
			var orders = await _context.Orders.ToListAsync();
			return orders;
		}

		public async Task<bool> DeleteOrder(int Id)
		{
			var getRow = await _context.Orders.Where(x => x.Id == Id).FirstOrDefaultAsync();
			if(getRow == null) return false;	
			 _context.Orders.Remove(getRow);	
			return true;
		}
	}
}
