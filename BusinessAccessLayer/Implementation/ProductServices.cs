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

namespace BusinessAccessLayer.Implementation
{
    public class ProductServices : IProductServices
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;



        public ProductServices(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<List<ProductModel>> ProductList()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }
        public async Task<bool> AddProduct(ProductViewModel model, string path)
        {
            var filePath = "/assets/Images/" + model.Image.FileName;

            var fullPath = path + filePath;
            UploadFile(model.Image, fullPath);
            if (model != null)
            {
                var newProduct = new ProductModel
                {
                    Name = model.ProductModel.Name,
                    Description = model.ProductModel.Description,
                    AddedDate = model.ProductModel.AddedDate,
                    Availability = model.ProductModel.Availability,
                    Price = model.ProductModel.Price,
                    Category = model.ProductModel.Category,
                    ImageURL = filePath
                };
                _context.Add(newProduct);
                await _context.SaveChangesAsync();
                return true;
            }
            else return false;
        }
        public void UploadFile(IFormFile file, string fullPath)
        {
            FileStream stream = new FileStream(fullPath, FileMode.Create);
            file.CopyTo(stream);
        }
        public async Task<bool> DeleteProduct(int Id, string path)
        {
            var row = await _context.Products.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (row != null)
            {
                var filePath = path + row.ImageURL;
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    _context.Products.Remove(row);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            return false;
        }

        

        // Post Method for EditProduct
        public async Task<bool> EditProduct(ProductViewModel model, string path)
        {
            var getRow = await _context.Products.Where(x => x.Id == model.ProductModel.Id).FirstOrDefaultAsync();
            var isImageValid = model.Image;
            if (getRow != null && isImageValid != null)
            {
                var filePathOld = path + getRow.ImageURL;
                if (File.Exists(filePathOld))
                {
                    File.Delete(filePathOld);

                    var filePath = "/assets/Images/" + model.Image.FileName;
                    var fullPath = path + filePath;
                    UploadFile(model.Image, fullPath);
                    getRow.Name = model.ProductModel.Name;
                    getRow.Description = model.ProductModel.Description;
                    getRow.AddedDate = model.ProductModel.AddedDate;
                    getRow.Availability  = model.ProductModel.Availability;
                    getRow.Price = model.ProductModel.Price;
                    getRow.Category = model.ProductModel.Category;
                    getRow.ImageURL = filePath;

                    return true;
                }
            }
            return false;
        }
        // Get Method for EditProduct
        public async Task<ProductModel> EditProduct(int  Id)
        {
            var getRow = await _context.Products.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if(getRow != null)
            {
                return getRow;
            }
            return null;
        }
        public async Task<ProductModel> MyProduct(int Id)
        {
            var getRow = await _context.Products.Where(x=>x.Id == Id).FirstOrDefaultAsync();
			if (getRow != null)
			{
				return getRow;
			}
			return null;
		}
    }
}
