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
    private readonly IOrderReviewRepository _orderReviewRepository;
    private readonly IRestaurantService _restaurantService;
    private readonly IMapper _mapper;


    public OrderService(IOrderRepository orderRepository,IOrderReviewRepository orderReviewRepository,IRestaurantService restaurantService, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _orderReviewRepository = orderReviewRepository;
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

    public async Task<ShowCourierOrderDto?> GetActiveCourierOrder(int courierId)
    {
        var order = await _orderRepository.GetActiveCourierOrder(courierId);
        if(order == null)
            return null;

        return _mapper.Map<ShowCourierOrderDto>(order);
    }
    
    public async Task<List<ShowOrderDto>> GetPendingOrdersByRestaurant(int restaurantId, int requestingUserId, string role)
    {
        await AuthorizeRestaurantAccess(restaurantId, requestingUserId, role);

        var orders = await _orderRepository.GetPendingOrdersByRestaurant(restaurantId);
        return _mapper.Map<List<ShowOrderDto>>(orders);
    }

    public async Task<List<ShowOrderDto>> GetAcceptedOrdersByRestaurant(int restaurantId, int requestingUserId, string role)
    {
        await AuthorizeRestaurantAccess(restaurantId, requestingUserId, role);

        var orders = await _orderRepository.GetAcceptedOrdersByRestaurant(restaurantId);
        return _mapper.Map<List<ShowOrderDto>>(orders);
    }

    public async Task<List<ShowOrderDto>> GetCanceledOrdersByRestaurant(int restaurantId, int requestingUserId, string role)
    {
        await AuthorizeRestaurantAccess(restaurantId, requestingUserId, role);

        var orders = await _orderRepository.GetCanceledOrdersByRestaurant(restaurantId);
        return _mapper.Map<List<ShowOrderDto>>(orders);
    }

    public async Task<ShowOrderDto> AcceptOrder(int orderId, AcceptOrderDto acceptOrderDto, int requestingUserId, string role)
    {
        var order = await GetOrderOrThrow(orderId);

        await AuthorizeRestaurantAccess(order.RestaurantId, requestingUserId, role);

        if (order.OrderStatus != OrderStatus.OnHold)
        {
            throw new BadRequestException($"Only orders with status OnHold can be accepted. Current status: {order.OrderStatus}");
        }

        order.OrderStatus = OrderStatus.Accepted;
        order.EstimatedReadyAt = acceptOrderDto.EstimatedReadyAt;

        var updatedOrder = await _orderRepository.UpdateOrder(order);
        return _mapper.Map<ShowOrderDto>(updatedOrder);
    }

    public async Task<ShowOrderDto> CancelOrder(int orderId, int requestingUserId, string role)
    {
        var order = await GetOrderOrThrow(orderId);

        await AuthorizeRestaurantAccess(order.RestaurantId, requestingUserId, role);

        if (order.OrderStatus != OrderStatus.OnHold)
        {
            throw new BadRequestException($"Only orders with status OnHold can be canceled. Current status: {order.OrderStatus}");
        }

        order.OrderStatus = OrderStatus.Canceled;

        var updatedOrder = await _orderRepository.UpdateOrder(order);
        return _mapper.Map<ShowOrderDto>(updatedOrder);
    }

    private async Task<Order> GetOrderOrThrow(int orderId)
    {
        var order = await _orderRepository.GetOrderById(orderId);
        if (order == null)
        {
            throw new NotFoundException(orderId);
        }
        return order;
    }

    private async Task AuthorizeRestaurantAccess(int restaurantId, int requestingUserId, string role)
    {
        var restaurant = await _restaurantService.GetRestaurantById(restaurantId);

        if (role == "Owner" && restaurant.OwnerId != requestingUserId)
        {
            throw new ForbiddenException("You do not own this restaurant.");
        }

        // TODO: Kada se prosiri entitet restorana sa Employees svojstvom
        // if (role == "Employee" && !restaurant.Employees.Any(e => e.UserId == requestingUserId))
        // throw new ForbiddenException("You are not employed at this restaurant.");
    }


    public async Task<OrderReviewDto> CreateOrderReviewAsync(int orderId, int customerId, OrderReviewDto orderReviewDto)
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId);

        if (order == null)
        {
            throw new NotFoundException(orderId);
        }

        if (order.OrderStatus != OrderStatus.Delivered)
        {
            throw new BadRequestException("You cannot leave a review for this order.");
        }

        if (order.CustomerId != customerId)
        {
            throw new ForbiddenException("You cannot leave a review for this order.");
        }

        var existingReview = await _orderReviewRepository.GetReviewByOrderId(orderId);
        if (existingReview != null)
        {
            throw new BadRequestException("You already reviewed this order.");
        }

        var review = new OrderReview
        {
            RestaurantGrade = orderReviewDto.RestaurantGrade,
            RestaurantComment = orderReviewDto.RestaurantComment,
            CourierGrade = orderReviewDto.CourierGrade,
            CourierComment = orderReviewDto.CourierComment,
            CreatedAt = DateTime.UtcNow,
            OrderId = orderId,
            CustomerId = customerId,
            RestaurantId = order.RestaurantId,
            CourierId = order.CourierId!.Value
        };

        var createdReview = await _orderReviewRepository.CreateOrderReviewAsync(review);

        return _mapper.Map<OrderReviewDto>(createdReview);
    }
}