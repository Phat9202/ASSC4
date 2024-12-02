using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phatdvph47227.Models
{
    public class CanHo
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Ten { get; set; }
        [Required]
        public double DienTich { get; set; }
        [Required]
        public string So { get; set; }
        [Required]
        public ToaNha ToaNha { get; set; }
    }
}
