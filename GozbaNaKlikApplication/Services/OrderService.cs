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
    private readonly ICourierService _courierService;
    private readonly IMapper _mapper;


    public OrderService(IOrderRepository orderRepository, IOrderReviewRepository orderReviewRepository, IRestaurantService restaurantService, IMapper mapper, ICourierService courierService)
    {
        _orderRepository = orderRepository;
        _orderReviewRepository = orderReviewRepository;
        _restaurantService = restaurantService;
        _courierService = courierService;
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
        if (order == null)
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

    public async Task<List<ShowOrderDto>> GetOrdersByCustomerId(int customerId, OrderStatus? status)
    {
        var orders = await _orderRepository.GetOrdersByCustomerId(customerId, status);
        return _mapper.Map<List<ShowOrderDto>>(orders);
    }

    public async Task<ShowOrderDto> AssignCourierToOrderAsync(int orderId, int requestingUserId, string role)
    {
        var order = await GetOrderOrThrow(orderId);
        await AuthorizeRestaurantAccess(order.RestaurantId, requestingUserId, role);

        if (order.OrderStatus != OrderStatus.Accepted)
        {
            throw new BadRequestException($"Only orders with status accepted can be assign to courier. Current status: {order.OrderStatus}");
        }
        var courier = await _courierService.GetAvailableCourierAsync();
        order.OrderStatus = OrderStatus.PickupInProgress;
        order.CourierId = courier.UserId;
        var updatedOrder = await _orderRepository.UpdateOrder(order);
        courier.IsAvailable = false;
        await _courierService.UpdateCourier(courier);

        return _mapper.Map<ShowOrderDto>(updatedOrder);
    }

    public async Task<ShowOrderDto> PickUpOrderAsync(int orderId, int userId)
    {
        var order = await GetOrderOrThrow(orderId);

        if (order.CourierId != userId)
        {
            throw new BadRequestException("This courier is not assigned to this order.");
        }

        if (order.OrderStatus != OrderStatus.PickupInProgress)
        {
            throw new BadRequestException($"Only orders with status PickupInProgress can be picked up. Current status: {order.OrderStatus}");
        }

        DateTime pickedUpAt = DateTime.UtcNow;
        if (pickedUpAt < order.EstimatedReadyAt)
        {
            throw new BadRequestException("Order is not ready yet.");
        }

        order.OrderStatus = OrderStatus.DeliveryInProgress;
        order.PickedUpAt = pickedUpAt;
        var updateOrder = await _orderRepository.UpdateOrder(order);
        return _mapper.Map<ShowOrderDto>(updateOrder);
    }

    public async Task<ShowOrderDto> OrderDeliveredAsync(int orderId, int userId)
    {
        var order = await GetOrderOrThrow(orderId);

        if (order.CourierId != userId)
        {
            throw new BadRequestException("This courier is not assigned to this order.");
        }

        if (order.OrderStatus != OrderStatus.DeliveryInProgress)
        {
            throw new BadRequestException($"Only orders with status DeliveryInProgress can be delivered. Current status: {order.OrderStatus}");
        }

        DateTime deliveredAt = DateTime.UtcNow;
        if (order.PickedUpAt == null)
        {
            throw new BadRequestException("Order is not picked up yet.");
        }
        if (deliveredAt < order.PickedUpAt)
        {
            throw new BadRequestException("Invalid delivery time.");
        }

        order.OrderStatus = OrderStatus.Delivered;
        order.DeliveredAt = deliveredAt;
        var updateOrder = await _orderRepository.UpdateOrder(order);
        var courier = await _courierService.GetCourierById(userId);
        courier.IsAvailable = true;
        await _courierService.UpdateCourier(courier);
        return _mapper.Map<ShowOrderDto>(updateOrder);
    }
}