using BreakfastBuffet.Data.Model;
using Microsoft.AspNetCore.SignalR;

namespace BreakfastBuffet.Hubs
{
  public class KitchenHub : Hub
  {
    public async Task sendMessage(ReservationModel user, string message)
    {
      await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
  }
}