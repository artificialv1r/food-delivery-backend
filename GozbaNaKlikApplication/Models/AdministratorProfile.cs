namespace GozbaNaKlikApplication.Models
{
    public class AdministratorProfile
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User Administrator { get; set; }
    }
}
