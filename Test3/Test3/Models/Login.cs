using System.ComponentModel.DataAnnotations;

namespace Test3.Models
{
    public class Login
    {
        [Key]
        public int Id { get; set; }
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
    }
}
