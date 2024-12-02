using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class Sach
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Ma { get; set; }
        public string Ten { get; set; }
        public int DonGia { get; set; }
        public string SoTrang { get; set; }
    }
}
