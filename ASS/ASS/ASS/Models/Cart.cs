using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ASS.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int AccountId { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
        [Required]
        public bool Status {  get; set; }
        [ForeignKey(nameof(AccountId))]
        public Account Accounts { get; set; }
        public ICollection<Cart_Detail> CartDetails { get; set; }
    }
}
