using Test.Models;

namespace Test.Models
{
    public class UserRole
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public Account User { get; set; }
        public string Roleld { get; set; }
        public Role Role { get; set; }
    }
}
