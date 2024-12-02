using System.ComponentModel.DataAnnotations;

namespace WebBanHang.Models
{
	public class UserModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Hay nhap User Name")]
		public string UserName { get; set; }
		[Required(ErrorMessage = "Hay nhap User Name"),EmailAddress]
		public string Email { get; set; }
		[DataType(DataType.Password),Required(ErrorMessage = "Hay nhap mat khau")]
		public string Password { get; set; }

	}
}
