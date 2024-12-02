namespace Test.Models
{
    public class Account
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public Cart Cart { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}