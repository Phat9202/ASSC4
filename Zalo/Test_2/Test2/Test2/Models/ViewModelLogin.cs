using System.ComponentModel.DataAnnotations;

namespace Test2.Models
{
    public class ViewModelLogin
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }
    }
}
