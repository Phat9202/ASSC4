using Microsoft.EntityFrameworkCore;
using WebBanHang.Models;

namespace WebBanHang.Repository
{
	public class SeedData
	{
		public static void SeedingData(DataContext _context)
		{
			_context.Database.Migrate();
			if(!_context.Products.Any())
			{
				CategoryModel macbook = new CategoryModel { Name= "macbook", Description = "May tinh pho bien hang dau", Status= 1};
				CategoryModel pc = new CategoryModel { Name = "Samsung", Description = "Laptop pho bien thu hai", Status = 1 };
				BrandModel apple = new BrandModel { Name = "Apple", Description = "Dien thoai pho bien hang dau", Status = 1 };
				BrandModel samsung = new BrandModel { Name = "Samsung", Description = "Dien thoai pho bien so hai", Status = 1 };
				_context.Products.AddRange(
					new ProductModel { Name = "Macbook", Quantity = 5, Description = "May tinh bang", Image = "1.jpg", Category = macbook, Brand = apple, Price = 1000 },
					new ProductModel { Name = "pc", Quantity = 10, Description = "Computer", Image = "2.jpg", Category = pc, Brand = samsung, Price = 2000 }
				);
			}
			_context.SaveChanges();
		}
	}
}
