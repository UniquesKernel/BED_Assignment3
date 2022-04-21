using BreakfastBuffet.Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace BreakfastBuffet.Hubs
{
  public class ChatHub : Hub
  {
    public async Task SendMessage()
    {
      await Clients.All.SendAsync("ReceiveMessage");
    }
  }
}