using AutoMapper;
using GozbaNaKlikApplication.DTOs.Meals;
using GozbaNaKlikApplication.DTOs.Orders;
using GozbaNaKlikApplication.Exceptions;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Models.Enums;
using GozbaNaKlikApplication.Models.Interfaces;
using GozbaNaKlikApplication.Services.Interfaces;

namespace GozbaNaKlikApplication.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IRestaurantService _restaurantService;
    private readonly IMapper _mapper;

    
    public OrderService(IOrderRepository orderRepository, IRestaurantService restaurantService, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _restaurantService = restaurantService;
        _mapper = mapper;
    }

    public async Task<ShowOrderDto> CreateOrder(CreateOrderDto orderDto, int customerId)
    {
        var restaurant = await _restaurantService.GetRestaurantById(orderDto.RestaurantId);
        
        var orderMeals = new List<OrderMeal>();
        decimal mealsPrice = 0;
        decimal deliveryPrice = 200;
        
        foreach (var mealDto in orderDto.MealsOrdered)
        {
            var meal = restaurant.Meals.FirstOrDefault(m => m.Id == mealDto.MealId);
            if (meal == null)
            {
                throw new NotFoundException(mealDto.MealId);
            }
    
            var orderMeal = new OrderMeal
            {
                MealId = meal.Id,
                Quantity = mealDto.Quantity,
                PriceAtOrder = meal.Price
            };
    
            orderMeals.Add(orderMeal);
            mealsPrice += meal.Price * mealDto.Quantity;
        }
        
        var order = new Order
        {
            CustomerId = customerId,
            RestaurantId = orderDto.RestaurantId,
            MealsOrdered = orderMeals,
            MealsPrice = mealsPrice,
            DeliveryPrice = deliveryPrice,
            TotalPrice = mealsPrice + deliveryPrice,
            OrderStatus = OrderStatus.OnHold,
            DeliveryStreet = orderDto.DeliveryStreet,
            DeliveryCity = orderDto.DeliveryCity,
            DeliveryStreetNumber = orderDto.DeliveryStreetNumber,
            DeliveryFloor = orderDto.DeliveryFloor,
            DeliveryApartment = orderDto.DeliveryApartment,
        };

        var createdOrder = await _orderRepository.CreateOrder(order);
        return _mapper.Map<ShowOrderDto>(createdOrder);
    }
    
}