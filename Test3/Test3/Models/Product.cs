using System.ComponentModel.DataAnnotations;

namespace Test3.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Note { get; set; }

        public Product(int id, string name, int price, string note)
        {
            Id = id;
            Name = name;
            Price = price;
            Note = note;
        }

        public Product()
        {
        }
    }
}
