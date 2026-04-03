using Microsoft.AspNetCore.SignalR;

namespace GozbaNaKlikApplication.Hubs
{
    public class TrackingHub : Hub
    {
        public async Task JoinOrderTracking(int orderId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"order-{orderId}");
        }

        public async Task LeaveOrderTracking(int orderId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"order-{orderId}");
        }
    }
}
