namespace GozbaNaKlikApplication.Models
{
    public class OwnerProfile
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
