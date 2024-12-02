﻿using System.ComponentModel.DataAnnotations;

namespace WebBanHang.Models
{
	public class ProductModel
	{
		[Key]
		public int Id { get; set; }
		[Required,MinLength(4,ErrorMessage = "yeu cau nhap ten san pham")]
		public string Name { get; set; }
		public int Quantity { get; set; }
		[Required, MinLength(4, ErrorMessage = "yeu cau nhap mo ta san pham")]
		public string Description { get; set; }
		[Required]
		[Range(0.01, double.MaxValue, ErrorMessage = "Gia phai lon hon 0")]
		public decimal Price { get; set; }	
		public int BrandId { get; set; }
		public int CategoryId { get; set; }
		public string Image {  get; set; }

		public BrandModel Brand { get; set; }
		public CategoryModel Category { get; set; }

	}
}
