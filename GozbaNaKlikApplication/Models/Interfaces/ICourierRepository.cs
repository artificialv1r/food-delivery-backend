using GozbaNaKlikApplication.DTOs.Courier;
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
        Task<PaginatedList<Order>> GetFilteredAndSortedDeliveredOrdersAsync(int courierId, OrderSearchQuery orderSearchQuery, int page = 1, int pageSize = 5);
    }
}