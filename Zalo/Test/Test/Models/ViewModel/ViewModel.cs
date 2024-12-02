using System.ComponentModel.DataAnnotations;

namespace Test.Models.ViewModel
{
    public class ViewModel
    {
        public class ViewModelLogin
        {
            [Required]
            public string username { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string password { get; set; }
        }
    }
}
