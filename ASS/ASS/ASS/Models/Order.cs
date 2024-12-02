using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ASS.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AccountId { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public string ShippingAddress { get; set; }

        public string Notes { get; set; }

        [ForeignKey(nameof(AccountId))]
        public Account Account { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }

}
