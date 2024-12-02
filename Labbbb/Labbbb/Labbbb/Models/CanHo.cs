using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labbbb.Models
{
    public class CanHo
    {
        [Key]
        public string Id { get; set; }
        public string Ten { get; set; }
        public string dientich { get; set; }
        public double So { get; set; }
        [ForeignKey(nameof(IdToaNha))]
        public string IdToaNha { get; set; }
        public ToaNha ToaNha { get; set; }
    }
}
