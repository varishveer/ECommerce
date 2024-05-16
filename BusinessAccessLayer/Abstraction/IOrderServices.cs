using ModelAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Abstraction
{
	public interface IOrderServices
	{
		public Task<bool> addProductToCart(int Id);
	}
}
