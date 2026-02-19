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

        CreateMap<CreateMealDto, Meal>();

        CreateMap<Meal, ShowMealDto>();

        CreateMap<UpdateMealDto, Meal>();

        CreateMap<Restaurant, ShowRestaurantDto>()
              .ForMember(owner => owner.OwnerUserName,
              opt => opt.MapFrom(src => src.Owner.User.Name + " " + src.Owner.User.Surname))
              .ForMember(dest => dest.Meals,
              opt => opt.MapFrom(src => src.Meals));

        CreateMap<User, UserPreviewDto>();
    }
}