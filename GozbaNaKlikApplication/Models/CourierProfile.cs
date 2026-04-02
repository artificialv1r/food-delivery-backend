namespace GozbaNaKlikApplication.Models
{
    public class CourierProfile
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public bool Status { get; set; } = false;//status ce biti vezan za radne sate, ukoliko kuriru nije pocelo radno vreme ili se zavrsilo status ce biti false
        public bool IsAvailable { get; set; } = true;
        public List<Order> Orders { get; set; }
        public List<CourierWorkingHours> CourierWorkingHours { get; set; }


        public double? CurrentLatitude { get; set; }
        public double? CurrentLongitude { get; set; }
    }
}
