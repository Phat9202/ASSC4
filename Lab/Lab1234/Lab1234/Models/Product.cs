using System.ComponentModel.DataAnnotations;

namespace Lab1234.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Category { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
