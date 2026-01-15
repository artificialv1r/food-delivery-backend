namespace GozbaNaKlikApplication.Models
{
    public class CourierProfile
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User Courier {  get; set; }
        public bool Status { get; set; } = false;
    }
}
