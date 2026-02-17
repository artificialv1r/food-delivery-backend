using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.Services.Interfaces
{
    public interface ICourierService
    {
        Task<CourierProfile> AddCourierAsync(CourierProfile courier);
    }
}