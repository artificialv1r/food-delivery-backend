namespace GozbaNaKlikApplication.Models
{
    public class OwnerProfile
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Restaurant> MyRestaurants { get; set; }
    }
}
