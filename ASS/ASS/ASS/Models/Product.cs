using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASS.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100, ErrorMessage = "Tên không thể dài hơn 100 ký tự.")]
        public string Ten { get; set; }
        [Required, Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0.")]
        public int SoLuong { get; set; }
        public string Mota { get; set; }
        [Required]
        public decimal Gia { get; set; }
        [Required]
        public string ManHinh { get; set; }
        [Required]
        public string BoNho { get; set; }
        [Required]
        public string HeDieuHanh { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public int ThuongHieuId { get; set; }
        [Required]
        public int PhanLoaiId { get; set; }
        [ForeignKey(nameof(ThuongHieuId))]
        public Brand ThuongHieu { get; set; }
        [ForeignKey(nameof(PhanLoaiId))]
        public Category PhanLoai { get; set; }
    }

}

