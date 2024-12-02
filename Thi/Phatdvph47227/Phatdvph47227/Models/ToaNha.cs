using System.ComponentModel.DataAnnotations;

namespace Phatdvph47227.Models
{
    public class ToaNha
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string DiaChi { get; set; }
        public List<CanHo> canHos { get; set; }
    }
}
