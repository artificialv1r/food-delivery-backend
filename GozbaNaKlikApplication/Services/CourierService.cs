using AutoMapper;
using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.DTOs.Courier;
using GozbaNaKlikApplication.Exceptions;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Models.Interfaces;
using GozbaNaKlikApplication.Repositories;
using GozbaNaKlikApplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GozbaNaKlikApplication.Services;

public class CourierService : ICourierService
{
    private readonly ICourierRepository _courierRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public CourierService(ICourierRepository courierRepository, IUserRepository userRepository, IMapper mapper)
    {
        _courierRepository = courierRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public async Task<CourierProfile> GetCourierById(int id)
    {
        return await _courierRepository.GetCourierByIdAsync(id);
    }
    public async Task<CourierProfile> AddCourierAsync(CourierProfile courier)
    {
        return await _courierRepository.AddNewCourierAsync(courier);
    }
    public async Task<CourierProfile> GetAvailableCourierAsync()
    {
        var courier =  await _courierRepository.GetAvailableCourierAsync();
        return courier;
    }
    public async Task<CourierWorkingHoursDto> AddCourireWorkingHoursAsync(CourierWorkingHoursDto courierWorkingHoursDto, int courierId)
    {
        var courier = await _courierRepository.GetCourierByIdAsync(courierId);
        double totalWeekHours = 0;
        if (courier == null)
        {
            throw new NotFoundException(courierId);
        }

        if (courierWorkingHoursDto.EndTime <= courierWorkingHoursDto.StartTime)
        {
            throw new BadRequestException("End time must be after start time.");
        }

        double shiftHours = (courierWorkingHoursDto.EndTime - courierWorkingHoursDto.StartTime).TotalHours;
        if (shiftHours > 10)
        {
            throw new BadRequestException("Daily shift can't be over 10 hours.");
        }

        if (courier.CourierWorkingHours == null || !courier.CourierWorkingHours.Any())
        {
            totalWeekHours = 0;
        }
        else
        {
             totalWeekHours = courier.CourierWorkingHours
            .Where(c => c.DayOfWeek >= DayOfWeek.Monday && c.DayOfWeek <= DayOfWeek.Sunday)
            .Sum(c => (c.EndTime - c.StartTime).TotalHours);
        }

        if (totalWeekHours > 40)
        {
            throw new BadRequestException("You can't have more than 40 hours per week.");
        }
        if (courierWorkingHoursDto.StartTime <= DateTime.Now.TimeOfDay && courierWorkingHoursDto.EndTime >= DateTime.Now.TimeOfDay
            && courierWorkingHoursDto.DayOfWeek == DateTime.Now.DayOfWeek) { courier.Status = true; }

        var courierWorkingHours = _mapper.Map<CourierWorkingHours>(courierWorkingHoursDto);
        courierWorkingHours.CourierId = courierId;

        await _courierRepository.AddCourireWorkingHoursAsync(courierWorkingHours);
        return _mapper.Map<CourierWorkingHoursDto>(courierWorkingHours);
    }
    public async Task<CourierProfile> UpdateCourier(CourierProfile courier)
    {
        return await _courierRepository.UpdateCourier(courier);
    }

    public async Task<PaginatedList<ShowDeliveredOrderDto>> GetFilteredAndSortedDeliveredOrdersAsync(int courierId, OrderSearchQuery orderSearchQuery, int page = 1, int pageSize = 5)
    {
        var orders = await _courierRepository.GetFilteredAndSortedDeliveredOrdersAsync(courierId, orderSearchQuery, page, pageSize);
        var ordersDto = orders.Items
            .Select(_mapper.Map<ShowDeliveredOrderDto>).ToList();
        return new PaginatedList<ShowDeliveredOrderDto>(ordersDto, orders.Count, orders.PageIndex, pageSize);
    }
}
