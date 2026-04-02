using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.Models.Interfaces
{
    public interface ICourierRepository
    {
        Task<CourierProfile> AddNewCourierAsync(CourierProfile courier);
        Task<CourierWorkingHours> AddCourireWorkingHoursAsync(CourierWorkingHours courierWorkingHours);
        Task<CourierProfile> GetCourierByIdAsync(int id);
        Task<CourierProfile> GetAvailableCourierAsync();
        Task<CourierProfile> UpdateCourier(CourierProfile courier);
        Task<CourierWorkingHours> GetCourierWorkingHoursByIdAsync(int id, int workingHoursId);
        Task<CourierWorkingHours> UpdateCourierWorkingHoursAsync(CourierWorkingHours courierWorkingHours);
    }
}