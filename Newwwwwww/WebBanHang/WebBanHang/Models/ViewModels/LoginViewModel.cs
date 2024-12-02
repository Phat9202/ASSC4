using System.ComponentModel.DataAnnotations;

namespace WebBanHang.Models.ViewModels
{
	public class LoginViewModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Hay nhap User Name")]
		public string UserName { get; set; }
		[Required(ErrorMessage = "Hay nhap Password")]
		public string Password { get; set; }
		public string ReturnUrl { get; set; }
	}
}
