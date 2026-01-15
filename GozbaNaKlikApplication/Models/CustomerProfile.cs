namespace GozbaNaKlikApplication.Models
{
    public class CustomerProfile
    {
        public int Id { get; set; }
        public int UserId {  get; set; }
        public User Customer { get; set; }

    }
}
