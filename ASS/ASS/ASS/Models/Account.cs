using System.ComponentModel.DataAnnotations;

namespace ASS.Models
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        [Required]
        [StringLength(50)]
        public string FullName { get; set; }
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required]
        [StringLength(50)]
        public string PassWord { get; set; }
        [Required]
        [StringLength(255)]
        public string Email { get; set; }
        [Required]
        [StringLength(10)]
        public string PhoneNumber { get; set; }
        public string? Adress { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        [Required]
        public string Role { get; set; } 
    }
}
