using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.DTOs.Courier
{
    public class CourierWorkingHoursDto
    {
        public int Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
