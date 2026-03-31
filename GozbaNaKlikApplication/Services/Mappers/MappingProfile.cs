using AutoMapper;
using GozbaNaKlikApplication.DTOs.Address;
using GozbaNaKlikApplication.DTOs.Auth;
using GozbaNaKlikApplication.DTOs.Meals;
using GozbaNaKlikApplication.DTOs.Orders;
using GozbaNaKlikApplication.DTOs.Restaurant;
using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.Services.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        CreateMap<CreateMealDto, Meal>();
        CreateMap<Meal, ShowMealDto>();
        CreateMap<UpdateMealDto, Meal>();
        CreateMap<MealsDto, Meal>().ReverseMap();

        CreateMap<UpdateRestaurantDto, Restaurant>().ReverseMap();
        CreateMap<Restaurant, ShowRestaurantDto>()
              .ForMember(owner => owner.OwnerUserName,
              opt => opt.MapFrom(src => src.Owner.User.Name + " " + src.Owner.User.Surname))
              .ForMember(dest => dest.Meals,
              opt => opt.MapFrom(src => src.Meals));

        CreateMap<User, UserPreviewDto>();

        CreateMap<Meal, ShowMealsDto>();
        CreateMap<CreateCustomerAddressDto, Address>().ReverseMap();
        CreateMap<Address, ShowAddressDto>();
        CreateMap<Address, UpdateAddressDto>().ReverseMap();

        CreateMap<CreateOrderDto, Order>();
        CreateMap<OrderMealDto, OrderMeal>();

        CreateMap<Order, ShowOrderDto>()
            .ForMember(dest => dest.RestaurantName,
                opt => opt.MapFrom(src => src.Restaurant.Name))
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.OrderStatus.ToString()));

        CreateMap<OrderMeal, ShowOrderMealDto>()
            .ForMember(dest => dest.MealName,
                opt => opt.MapFrom(src => src.Meal.Name));

        CreateMap<Order, ShowCourierOrderDto>()
            .ForMember(dest => dest.Meals, opt => opt.MapFrom(src =>
                src.MealsOrdered.Select(om => new CourierOrderMealDto
                {
                    MealName = om.Meal.Name,
                    Quantity = om.Quantity
                }).ToList()))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerProfile.User.Name))
            .ForMember(dest => dest.CustomerSurname, opt => opt.MapFrom(src => src.CustomerProfile.User.Surname))
            .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.CustomerProfile.User.Email))
            .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus.ToString())); 

    }
}