using GozbaNaKlikApplication.DTOs.Courier;
using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.Services.Interfaces
{
    public interface ICourierService
    {
        Task<CourierProfile> AddCourierAsync(CourierProfile courier);
        Task<CourierWorkingHoursDto> AddCourireWorkingHoursAsync(CourierWorkingHoursDto courierWorkingHoursDto, int courierId);
        Task<CourierProfile> GetAvailableCourierAsync();
        Task<CourierProfile> UpdateCourier(CourierProfile courier);
        Task<CourierProfile> UpdateCourierLocationAsync(int courierId, double latitude, double longitude);
        Task<CourierProfile> GetCourierById(int id);
        Task<UpdateCourierWorkingHoursDto> UpdateCourierWorkingHoursAsync(UpdateCourierWorkingHoursDto courierWorkingHours, int courierId, int workingHoursId);
        Task<PaginatedList<ShowDeliveredOrderDto>> GetFilteredAndSortedDeliveredOrdersAsync(int courierId, OrderSearchQuery orderSearchQuery, int page = 1, int pageSize = 5);
    }
}