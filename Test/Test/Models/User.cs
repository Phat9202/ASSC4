using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Hobbies { get; set; }
        public string Country { get; set; }

        public User(string name, int age, string gender, string hobbies, string country)
        {
            Name = name;
            Age = age;
            Gender = gender;
            Hobbies = hobbies;
            Country = country;
        }
        public User()
        {
        }
    }
}
