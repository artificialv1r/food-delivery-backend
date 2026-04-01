namespace GozbaNaKlikApplication.Models
{
    public class CourierWorkingHours
    {
        public int Id { get; set; }
        public int CourierId { get; set; }
        public CourierProfile CourierProfile { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
