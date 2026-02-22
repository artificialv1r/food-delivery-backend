using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.Models.Interfaces
{
    public interface ICourierRepository
    {
        Task<CourierProfile> AddNewCourierAsync(CourierProfile courier);
    }
}