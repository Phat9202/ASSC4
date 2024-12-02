using System.ComponentModel.DataAnnotations;

namespace WebBanHang.Models
{
	public class BrandModel
	{
		[Key]
		public int Id { get; set; }
		[Required,MinLength(4,ErrorMessage ="Yeu cau nhap ten danh muc")]
		public string Name { get; set; }
		[Required, MinLength(4, ErrorMessage = "Yeu cau nhap mo ta danh muc")]
		public string Description { get; set; }
		public int Quantity { get; set; }
		public int Status { get; set; }
	}
}
