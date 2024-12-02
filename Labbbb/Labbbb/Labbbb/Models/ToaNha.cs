namespace Labbbb.Models
{
    public class ToaNha
    {
        public string  Id { get; set; }
        public string  DiaChi { get; set; }
        public ICollection<CanHo> canHos { get; set; }
    }
}
