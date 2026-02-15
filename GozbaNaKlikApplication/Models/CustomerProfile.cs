namespace GozbaNaKlikApplication.Models
{
    public class CustomerProfile
    {
        public int UserId {  get; set; }
        public User User { get; set; }
        public ICollection<Allergen>?  CustomerAllergens { get; set; }

    }
}
