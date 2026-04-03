using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.Hubs;
using GozbaNaKlikApplication.Models.Enums;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace GozbaNaKlikApplication.Services
{
    public class CourierLocationBackgroundService : BackgroundService
    {
        private readonly IHubContext<TrackingHub> _hubContext;
        private readonly IServiceScopeFactory _scopeFactory;

        public CourierLocationBackgroundService ( IHubContext<TrackingHub> hubContext, IServiceScopeFactory scopeFactory)
        {
            _hubContext = hubContext;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync (CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await BroadcastCourierLocations();
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }

        private async Task BroadcastCourierLocations ()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var activeOrders = await context.Orders
                .Include(o => o.CourierProfile)
                .Where(o =>
                    o.CourierId != null &&
                    (o.OrderStatus == OrderStatus.PickupInProgress ||
                     o.OrderStatus == OrderStatus.DeliveryInProgress) &&
                    o.CourierProfile!.CurrentLatitude != null &&
                    o.CourierProfile!.CurrentLongitude != null)
                .ToListAsync();

            foreach ( var order in activeOrders )
            {
                await _hubContext.Clients
                    .Group($"order-{order.Id}")
                    .SendAsync("ReceiveCourierLocation", new 
                    {
                        orderId = order.Id,
                        latitude = order.CourierProfile!.CurrentLatitude,
                        longitude = order.CourierProfile!.CurrentLongitude,
                        orderStatus = order.OrderStatus.ToString()
                    });
            }
        }
    }
}
