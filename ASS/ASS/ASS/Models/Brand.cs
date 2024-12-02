using System.ComponentModel.DataAnnotations;

namespace ASS.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string BrandName { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
