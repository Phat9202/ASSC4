namespace Bai4.Models
{
    public class Custumer
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Adresss { get; set; }
        public Custumer()
        {
        }
        public Custumer(int id, string name, string adresss)
        {
            this.id = id;
            Name = name;
            Adresss = adresss;
        }
 

    }
}
