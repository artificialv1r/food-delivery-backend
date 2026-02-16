using AutoMapper;
using GozbaNaKlikApplication.DTOs.Auth;
using GozbaNaKlikApplication.DTOs.Meals;
using GozbaNaKlikApplication.DTOs.Restaurant;
using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.Services.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Restaurant, ShowRestaurantDto>()
              .ForMember(owner => owner.OwnerUserName,
              opt => opt.MapFrom(src => src.Owner.User.Name + " " + src.Owner.User.Surname)
              );

        CreateMap<User, UserPreviewDto>();

        CreateMap<Meal, ShowMealsDto>();
    }
}