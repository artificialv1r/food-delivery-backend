namespace GozbaNaKlikApplication.Models
{
    public class CourierProfile
    {
        public int UserId { get; set; }
        public User User {  get; set; }
        public bool Status { get; set; } = false;
    }
}
