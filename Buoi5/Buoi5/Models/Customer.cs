namespace Bai5.Models
{
    public class Customer
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Adresss { get; set; }
        public Customer()
        {
        }
        public Customer(int id, string name, string adresss)
        {
            this.id = id;
            Name = name;
            Adresss = adresss;
        }


    }
}
