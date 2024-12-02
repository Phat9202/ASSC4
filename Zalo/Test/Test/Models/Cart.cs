namespace Test.Models
{
    public class Cart
    {
        public int Id { get; set; }
        // Quan hệ one-to-one với Account
        public string UserId { get; set; }
        public Account User { get; set; }
        public ICollection<CartDetail> CartDetails { get; set; }
    }

}
